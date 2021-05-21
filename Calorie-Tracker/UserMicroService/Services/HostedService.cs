using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserMicroService.Contracts;
using UserMicroService.Models;

namespace UserMicroService.Services
{
    public class HostedService : IHostedService
    {
        //private readonly UserManager<User> userManager;
        private readonly IEmailService _emailService;

        public HostedService(IEmailService emailService)
        {
            //this.userManager = userManager;
            _emailService = emailService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(TaskRoutine, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return null;
        }
        public Task TaskRoutine()
        {
            while (true)
            {
                if (DateTime.Now.Date.Day != 1)
                {
                    var dateToSend = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
                    var wait = dateToSend.Date - DateTime.Now.Date;
                    Thread.Sleep(wait);
                }

                //var users = await userManager.Users.Where(x => x.EmailConfirmed).ToListAsync();
                //foreach (var user in users)
                //{
                //    await _emailService.SendEmailAsync(user.Email, "Hello", $"There is 1 day on the month!");
                //}

                Thread.Sleep(24 * 60 * 60 * 1000);
            }
        }
    }
}
