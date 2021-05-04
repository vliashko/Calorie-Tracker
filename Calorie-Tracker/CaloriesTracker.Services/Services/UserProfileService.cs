using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public UserProfileService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DayForChartDto>> GetDataForChart(Guid userId, int countDays)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            var eatings = await repositoryManager.Eating.GetAllEatingsForUserAsync(userId, trackChanges: false);
            var activities = await repositoryManager.Activity.GetAllActivitiesForUserAsync(userId, trackChanges: false);
            var resultEatings = eatings
                .Where(x => (DateTime.Now.Date - x.Moment.Date).Days < countDays)
                .GroupBy(x => x.Moment.Date)
                .Select(x => 
                    new DayForChartDto { Day = x.Key, CurrentCalories = x.Sum(k => k.TotalCalories) });
            var resultActivities = activities
                .Where(x => (DateTime.Now.Date - x.Moment.Date).Days < countDays)
                .GroupBy(x => x.Moment.Date)
                .Select(x =>
                    new DayForChartDto { Day = x.Key, CurrentCalories = x.Sum(k => k.TotalCaloriesSpent) });

            var result = new List<DayForChartDto>();
            for (int i = 0; i < countDays; i++)
            {
                var curDate = DateTime.Now.Date - new TimeSpan(24 * i, 0, 0);
                var resultAdd = new DayForChartDto { Day = curDate, CurrentCalories = 0 };
                var tempEat = resultEatings.SingleOrDefault(x => x.Day == curDate);
                var tempAct = resultActivities.SingleOrDefault(x => x.Day == curDate);
                if (tempEat != null)
                    resultAdd.CurrentCalories += tempEat.CurrentCalories;
                if (tempAct != null)
                    resultAdd.CurrentCalories -= tempAct.CurrentCalories;
                result.Add(resultAdd);
            }

            return result.OrderBy(x => x.Day);
        }

        public async Task<UserProfileForReadDto> GetUserProfileByUserId(string userId)
        {
            var users = await repositoryManager.User.GetAllUsersAsync(false);
            var user = users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                return null;
            var userDto = mapper.Map<UserProfileForReadDto>(user);
            return userDto;
        }
    }
}
