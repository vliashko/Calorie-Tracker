using CaloriesTracker.Contracts;
using CaloriesTracker.Contracts.Authentication;
using CaloriesTracker.Entities;
using CaloriesTracker.LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using CaloriesTracker.Repositories;
using CaloriesTracker.Repositories.Authentication;
using System.IO;
using CaloriesTracker.Api.Filter;
using CaloriesTracker.Api.Extensions;
using CaloriesTracker.Services.Interfaces;
using CaloriesTracker.Services.Services;
using CaloriesTracker.Services;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace CaloriesTracker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureSwagger();
            services.ConfigureJWT(Configuration);

            services.AddDbContext<RepositoryDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), 
                    b => b.MigrationsAssembly("CaloriesTracker.Api")));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddControllers().AddNewtonsoftJson();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);

            app.UseStaticFiles();
            if(!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Calorie Tracker API");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "ClientApp");

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
