using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using News.DataAccess.Database;

namespace News.API
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
            services.AddMediatR(typeof(ArticleController).Assembly);

            services.AddControllers();

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


            services.AddDbContext<newsaggregatordataContext>(config => config.UseMySql(connectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
