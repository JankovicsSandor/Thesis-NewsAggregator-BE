using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using User.BussinessLogic.AutoMapper;
using User.Data.Database;
using User.DataAccess.UserAcccess;

namespace User.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebEnv = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebEnv { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHealthChecks();

            var connectionString = Configuration.GetSection("MYSQLCONNSTR").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Database connection string is not correct.");
            }

            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile<AutoMapperProfile>();
              });

            services.AddSingleton(typeof(IMapper), mapperConfig.CreateMapper());


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddDbContext<UserContext>(config => config.UseMySql(connectionString));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (WebEnv.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/api/user/health_check");
                endpoints.MapControllers();
            });
        }
    }
}
