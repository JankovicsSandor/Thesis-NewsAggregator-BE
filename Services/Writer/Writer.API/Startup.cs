using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBusRabbitMQ.Abstractions;
using EventBusRabbitMQ.ServiceCollectionExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Writer.BussinessLogic.EventHandler;
using Writer.BussinessLogic.ExternalDataProvider.NewsData;
using Writer.DataAccess.Database;
using Writer.Shared.Events;

namespace Writer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<INewsHttpClient, NewsHttpClient>();

            services.AddSingleton<IMongoDatabaseService, MongoDatabaseService>();

            services.AddTransient<NewsGroupDoneEventHandler>();

            //Add evnet bus dependency services
            services.AddEventBus(Configuration);
            services.AddIntegrationServices(Configuration);

            services.AddHealthChecks();
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Subscribe for rabbitmq event
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<NewsGroupDoneEvent, NewsGroupDoneEventHandler>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/api/news/write/health_check");
                endpoints.MapControllers();
            });
        }
    }
}
