using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraHead
{
    public class FileReaderWorker : BackgroundService
    {
        private string _path;
        private int _delay;
        private readonly ILogger<FileReaderWorker> _logger;
        private readonly HydraApiService _hydra;

        public FileReaderWorker(ILogger<FileReaderWorker> logger, HydraApiService hydra)
        {
            // setup any content we need
            _path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Test.txt");
            _delay = 10000;
            _logger = logger;
            _hydra = hydra;
        }

        protected string? ScanForToken(string token)
        {
            string contents = File.ReadAllText(_path);

            string? foundLine = null;

            foreach (var line in contents.Split("\n"))
            {
                if (line.StartsWith(token))
                {
                    foundLine = line;
                    break;
                }
            }

            return foundLine;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var token = "[ERROR]";
                var foundError = ScanForToken(token);

                if(foundError == null)
                {
                    _logger.LogInformation("No Errors Found.");
                }
                else
                {
                    // tell the hydra api what we found
                    _logger.LogCritical(
                        $"Found File at path: {_path}\n" +
                        $"\tToken: {token}\n" +
                        $"\tLine: {foundError}\n"
                    );


                    // create an event
                    var evt = new Event
                    {
                        EventId = 0,
                        Description = foundError,
                        Type = EventType.LogFileMessage,
                        CategoryId = 2,
                        Timestamp = DateTime.Now,
                        HostAddress = Environment.MachineName
                    };

                    // call the api with the event
                    var result = await _hydra.Post(evt);

                    if (result == null)
                    {
                        throw new HttpRequestException("Hydra Unavailable");
                    }
                }

                // close the file
                await Task.Delay(_delay);
            }
        }
    }
}
