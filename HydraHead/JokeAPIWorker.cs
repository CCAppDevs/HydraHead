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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // TODO: add support for arbitrary api calls
            // TODO: detect when a call is missed and add it as an error event
            // TODO: setup http client as a service instead of direct
            // TODO: Add a way to adjust how often the api is called (timeout) and to chose if we want a specific info/warn/error level


            while (!stoppingToken.IsCancellationRequested)
            {
                // this gets data from the api
                var joke = await _httpService.Get();

                if (joke == null)
                {
                    _logger.LogError("No joke found");
                    return;
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
                }

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation(joke.joke);
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
