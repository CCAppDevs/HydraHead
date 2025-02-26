using Library;

namespace HydraHead
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<HydraApiService>();
            //builder.Services.AddSingleton<JokeService>(js => new JokeService("https://geek-jokes.sameerkumar.website"));
            builder.Services.AddSingleton<JokeService>(js => new JokeService("https://localhost"));
            builder.Services.AddHostedService<JokeAPIWorker>();

            var host = builder.Build();
            host.Run();
        }
    }
}