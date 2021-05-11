using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
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

        public async Task<UserProfileForReadDto> CreateUserProfileForUserAsync(string id, UserProfileForCreateDto userDto)
        {
            var userEntity = mapper.Map<UserProfile>(userDto);
            userEntity.UserId = id;
            repositoryManager.User.CreateUserProfile(userEntity);
            await repositoryManager.SaveAsync();
            var userView = mapper.Map<UserProfileForReadDto>(userEntity);
            return userView;
        }

        public async Task<IEnumerable<DayForChartDto>> GetDataForChartAsync(Guid id, int countDays)
        {
            var user = await repositoryManager.User.GetUserProfileAsync(id, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return null;
            }
            var eatings = await repositoryManager.Eating.GetAllEatingsForUserPerDays(id, countDays, trackChanges: false);
            var activities = await repositoryManager.Activity.GetAllActivitiesForUserPerDays(id, countDays, trackChanges: false);
            var resultEatings = eatings
                .GroupBy(x => x.Moment.Date)
                .Select(x =>
                    new DayForChartDto { Day = x.Key, CurrentCalories = x.Sum(k => k.TotalCalories) });
            var resultActivities = activities
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

        public async Task<UserProfileForReadDto> GetUserProfileByUserIdAsync(string id)
        {
            var user = await repositoryManager.User.GetUserProfileByUserIdAsync(id, false);
            if (user == null)
                return null;
            var userDto = mapper.Map<UserProfileForReadDto>(user);
            return userDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateUserProfileAsync(Guid id, JsonPatchDocument<UserProfileForUpdateDto> patchDoc)
        {
            var userEntity = await repositoryManager.User.GetUserProfileAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"UserProfile with id: {id} doesn't exist in the database" };
            }
            var userToPatch = mapper.Map<UserProfileForUpdateDto>(userEntity);
            patchDoc.ApplyTo(userToPatch);
            mapper.Map(userToPatch, userEntity);
            await repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateUserProfileAsync(Guid id, UserProfileForUpdateDto userDto)
        {
            var userEntity = await repositoryManager.User.GetUserProfileAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"UserProfile with id: {id} doesn't exist in the database" };
            }
            mapper.Map(userDto, userEntity);
            await repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
