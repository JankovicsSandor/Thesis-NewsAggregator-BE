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

namespace ResourceConfiguration.API
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

            var dbserver = Environment.GetEnvironmentVariable("dbserver");
            var dbuser = Environment.GetEnvironmentVariable("dbuser");
            var dbpw = Environment.GetEnvironmentVariable("dbpw");
            var dbname = Environment.GetEnvironmentVariable("dbname");
            var port = Environment.GetEnvironmentVariable("port");

            if (string.IsNullOrEmpty(dbserver) ||
                string.IsNullOrEmpty(dbuser) ||
                string.IsNullOrEmpty(port) ||
                string.IsNullOrEmpty(dbpw) ||
                string.IsNullOrEmpty(dbname))
            {
                throw new Exception("Database connection string is not correct.");
            }

            var connectionString = $"Server={dbserver};port={port};user id={dbuser};password={dbpw};database={dbname}";


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

            app.UseHttpsRedirection();
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
