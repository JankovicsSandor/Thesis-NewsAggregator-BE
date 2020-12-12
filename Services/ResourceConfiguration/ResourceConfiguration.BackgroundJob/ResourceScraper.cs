using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ResourceConfiguration.BackgroundJob.Worker;

namespace ResourceConfiguration.BackgroundJob
{
    public class ResourceScraper : IHostedService
    {
        private int executionCount = 0;
        private readonly ILogger<ResourceScraper> _logger;
        private readonly IServiceProvider _services;
        private Timer _timer;
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();


        public ResourceScraper(IServiceProvider services, ILogger<ResourceScraper> logger)
        {
            _logger = logger;
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            string delayAmount = Environment.GetEnvironmentVariable("APPSETTING_BACKGROUND_DELAY_SEC");
            int amount = 0;
            if (string.IsNullOrEmpty(delayAmount))
            {
                throw new Exception($"Delay value is empty");
            }
            if (!int.TryParse(delayAmount, out amount))
            {
                throw new Exception($"Delay value is not a number value: {delayAmount}");
            }

            _timer = new Timer(DoWork, null, TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(amount));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
            _executingTask = ExecuteTaskAsync(_stoppingCts.Token);
        }

        private async Task ExecuteTaskAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IResourceDownloader>();

                    await scopedProcessingService.ProcessResources();
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);
            _timer.Dispose();
            return Task.CompletedTask;
        }
    }
}
