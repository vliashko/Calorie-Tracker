using ActivityMicroService.Contracts;
using ActivityMicroService.Services;
using AzureFunctions;
using EatingMicroService.Contracts;
using EatingMicroService.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UserMicroService.Contracts;
using UserMicroService.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IActivityService, ActivityService>();
            builder.Services.AddScoped<IEatingService, EatingService>();
            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
