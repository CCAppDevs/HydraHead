using Library;

namespace HydraHead
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // C:\HydraHead.exe file c:\Path\To\Filename.txt
            Console.WriteLine("HydraHead executed with these arguments:");
            foreach (string item in args)
            {
                Console.WriteLine(item);
            }
            
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<HydraApiService>();
            
            // determine which head to start
            if (args[0] == "file")
            {
                builder.Services.AddHostedService<FileReaderWorker>();
            }
            else if (args[0] == "joke")
            {
                builder.Services.AddSingleton<JokeService>(js => new JokeService("https://geek-jokes.sameerkumar.website"));
                builder.Services.AddHostedService<JokeAPIWorker>();
            }

            var host = builder.Build();
            host.Run();
        }
    }
}