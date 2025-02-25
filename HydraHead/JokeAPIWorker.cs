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

        public JokeAPIWorker(ILogger<JokeAPIWorker> logger, JokeService http)
        {
            _logger = logger;
            _httpService = http;
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

                //response.EnsureSuccessStatusCode();

                //var jsonResponse = await response.Content.ReadAsStringAsync();


                //// create an event based on the data from the api
                //using StringContent jsonContent = new(
                //    JsonSerializer.Serialize(new Event
                //    {
                //        Description = jsonResponse,
                //        Type = EventType.ApiCall,
                //        CategoryId = 1,
                //        HostAddress = "https://geek-jokes.sameerkumar.website/api?format=json"
                //    }),
                //    Encoding.UTF8,
                //    "application/json");

                //// post that as a new event on the database
                //using HttpResponseMessage eventResponse = await eventClient.PostAsync("api/events/", jsonContent);

                //eventResponse.EnsureSuccessStatusCode();


                // output for logging
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation(joke.joke);
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
