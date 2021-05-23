using ActivityMicroService.Contracts;
using EatingMicroService.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.WebJobs;
using System.Linq;
using UserMicroService.Contracts;
using UserMicroService.DataTransferObjects;
using UserMicroService.Models;

namespace AzureFunctions
{
    public class MonthlyReport
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IActivityService _activityService;
        private readonly IEatingService _eatingService;

        public MonthlyReport(IEmailService emailService,
                             IUserService userService, 
                             IActivityService activityService, 
                             IEatingService eatingService)
        {
            _emailService = emailService;
            _userService = userService;
            _activityService = activityService;
            _eatingService = eatingService;
        }

        [FunctionName("MonthlyReport")]
        public async void Run([TimerTrigger("* * * 23 * *")] TimerInfo myTimer)
        {
            var users = await _userService.GetUsersPaginationAsync(1, 1, new UserSearchModelDto { });
            foreach (var user in users)
            {
                var acts = await _activityService.GetDataForChartAsync(user.UserProfile.Id, 31);
                var eats = await _eatingService.GetDataForChartAsync(user.UserProfile.Id, 31);
                var list = eats.Zip(acts, (e, a) => 
                    new DayForChartDto { Day = e.Day, CurrentCalories = e.CurrentCalories - a.CurrentCalories });
                
            }
        }
    }
}
