using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResourceConfiguration.API.Controllers;
using ResourceConfiguration.API.NetworkClient;
using ResourceConfiguration.BackgroundJob;
using ResourceConfigurator.DataAccess.Database;
using Serilog;
using Microsoft.EntityFrameworkCore;
using System;
using ResourceConfigurator.NetworkClient;
using ResourceConfigurator.NetworkClient.SyndicationFeedReader;
using System.Collections;

namespace ResourceConfiguration.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configroot)
        {
            var configBuilder = new ConfigurationBuilder();

            configBuilder.AddEnvironmentVariables();
            configBuilder.SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json");

            Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMediatR(typeof(ConfigurationController).Assembly);

            services.AddHttpClient<IResourceToDataNetworkClient, ResourceToDataNetworkClient>();

            services.AddTransient<IFeedReader, FeedReader>();

            services.AddHostedService<ResourceScraper>();


            // Add Cors
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSingleton<IGetResourceNetworkClient, GetResourceNetworkClient>();

            // var connectionString = $"Server={dbserver};port={port};user id={dbuser};password={dbpw};database={dbname}";
            var connectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_CONN");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Database connection string is not correct.");
            }

            services.AddDbContext<newsaggregatorresourceContext>(config => config.UseMySql(connectionString));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
