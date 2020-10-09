using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResourceConfiguration.BackgroundJob.EventBus;

namespace ResourceConfiguration.BackgroundJob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddEventBus(hostContext.Configuration);

                    services.AddHostedService<ResourceScraper>();
                });
    }
}
