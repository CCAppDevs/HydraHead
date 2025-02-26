using System.Net.Http;
using System.Text.Json;
using System.Text;
using Library;

namespace HydraHead
{
    public class JokeAPIWorker : BackgroundService
    {
        private readonly ILogger<JokeAPIWorker> _logger;

        private JokeService _httpService;
        private HydraApiService _hydra;

        public JokeAPIWorker(ILogger<JokeAPIWorker> logger, JokeService http, HydraApiService hydra)
        {
            _logger = logger;
            _httpService = http;
            _hydra = hydra;
        }

        protected async void PauseAsync(int delay)
        {
            await Task.Delay(delay, CancellationToken.None);
        }

        private void SendErrorToHydra(Exception ex)
        {
            // create a new Event with our data we received from the joke api
            var evt = new Event
            {
                EventId = 0,
                CategoryId = 1,
                Description = ex.Message,
                Type = EventType.ApiCallError,
                Timestamp = DateTime.Now,
                HostAddress = _httpService._baseUrl + "/api?format=json"
            };

            // call the hydra service and post a new event
            var response = _hydra.Post(evt);

            // check to see if it was a success
            if (response == null)
            {
                _logger.LogCritical("could not post an event to hydra");
                PauseAsync(10000);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // TODO: detect when a call is missed and add it as an error event
            // TODO: setup http client as a service instead of direct
            // TODO: Add a way to adjust how often the api is called (timeout) and to chose if we want a specific info/warn/error level

            while (!stoppingToken.IsCancellationRequested)
            {
                Joke joke = null;
                
                try
                {
                    // this gets data from the api
                    joke = await _httpService.Get();
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.Message);
                    SendErrorToHydra(e);
                    PauseAsync(10000);
                    continue;
                }


                if (joke == null)
                {
                    _logger.LogError("No joke found");
                    PauseAsync(10000);
                    continue;
                }


                // create a new Event with our data we received from the joke api
                var evt = new Event
                {
                    EventId = 0,
                    CategoryId = 1,
                    Description = joke.joke,
                    Type = EventType.ApiCall,
                    Timestamp = DateTime.Now,
                    HostAddress = _httpService._baseUrl + "/api?format=json"
                };

                // call the hydra service and post a new event
                var response = _hydra.Post(evt);

                // check to see if it was a success
                if (response == null)
                {
                    _logger.LogCritical("could not post an event to hydra");
                    PauseAsync(10000);
                    continue;
                }

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation(joke.joke);
                }
                PauseAsync(10000);
            }
        }


    }
}
