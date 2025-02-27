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

        public FileReaderWorker(ILogger<FileReaderWorker> logger)
        {
            // setup any content we need
            _path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Test.txt");
            _delay = 1000;
            _logger = logger;
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

                    // log the event with the api.
                }

                // close the file
                await Task.Delay(_delay);
            }
        }
    }
}
