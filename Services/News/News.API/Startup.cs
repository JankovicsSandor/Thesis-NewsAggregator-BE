using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMQ.Abstractions;
using EventBusRabbitMQ.ServiceCollectionExtension;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using News.API.Controllers;
using News.BussinessLogic;
using News.BussinessLogic.EventHandler;
using News.DataAccess.Database;
using News.Shared.Events;

namespace News.API
{
    public class Startup
    {
        public IWebHostEnvironment WebhostEnv { get; }
        public IConfiguration Configuration { get; }


        public Startup(IWebHostEnvironment env, IConfiguration configroot)
        {
            WebhostEnv = env;
            Configuration = configroot;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            services.AddAutoMapper(typeof(AddNewArticleEventHandler));
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMediatR(typeof(ArticleController).Assembly);
            services.AddMediatR(typeof(AddNewArticleEventHandler).Assembly);

            services.AddHealthChecks();

            services.AddEventBus(Configuration);
            services.AddIntegrationServices(Configuration);

            services.AddControllers().AddNewtonsoftJson();

            var connectionString = Configuration.GetSection("MYSQLCONNSTR").Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Database connection string is not correct.");
            }

            services.AddTransient<AddNewArticleEventHandler>();
                
            // Add Cors
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddDbContext<newsaggregatordataContext>(config => config.UseMySql(connectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (WebhostEnv.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Subscribe for rabbitmq event
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<AddNewArticleEvent, AddNewArticleEventHandler>();

            app.UsePathBase("/api/news/");

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/api/news/health_check");
                endpoints.MapControllers();
            });
        }
    }
}
