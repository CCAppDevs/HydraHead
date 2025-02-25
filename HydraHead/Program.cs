using Library;

namespace HydraHead
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<JokeService>(js => new JokeService("https://geek-jokes.sameerkumar.website"));
            builder.Services.AddHostedService<JokeAPIWorker>();

            var host = builder.Build();
            host.Run();
        }
    }
}