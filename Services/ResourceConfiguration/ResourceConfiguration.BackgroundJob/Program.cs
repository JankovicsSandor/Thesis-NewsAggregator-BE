using EventBusRabbitMQ.ServiceCollectionExtension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

                    IConfiguration Configuration = hostContext.Configuration;
                    services.AddEventBus(Configuration);
                    services.AddIntegrationServices(Configuration);

                    services.AddHostedService<ResourceScraper>();
                });
    }
}
