using ActivityMicroService.Contracts;
using ActivityMicroService.DataTransferObjects;
using ActivityMicroService.Models;
using AutoMapper;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityMicroService.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repository;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActivityForReadDto> CreateActivityForUserProfileAsync(Guid id, ActivityForCreateDto activityDto)
        {
            var activityEntity = _mapper.Map<Activity>(activityDto);
            activityEntity.UserProfileId = id;
            _repository.CreateActivity(activityEntity);
            var activityView = _mapper.Map<ActivityForReadDto>(activityEntity);
            await _repository.SaveAsync();
            return activityView;
        }

        public async Task<MessageDetailsDto> DeleteActivityAsync(Guid activityId)
        {
            var activity = await _repository.GetActivityAsync(activityId, trackChanges: false);
            if (activity == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Activity with id: {activityId} doesn't exist in the database" };

            }
            _repository.DeleteActivity(activity);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<IEnumerable<ActivityForReadDto>> GetActivitiesForUserProfileForDateAsync(Guid id)
        {
            DateTime dateTime = DateTime.Now;
            var activities = await _repository.GetAllActivitiesForUserForDateAsync(id, dateTime, trackChanges: false);
            var activitiesDto = _mapper.Map<IEnumerable<ActivityForReadDto>>(activities);
            return activitiesDto;
        }

        public async Task<IEnumerable<ActivityForReadDto>> GetActivitiesForUserProfilePerDaysAsync(Guid id, int days)
        {
            var activities = await _repository.GetAllActivitiesForUserPerDays(id, days, trackChanges: false);
            var activitiesDto = _mapper.Map<IEnumerable<ActivityForReadDto>>(activities);
            return activitiesDto;
        }

        public async Task<ActivityForReadDto> GetActivityAsync(Guid activityId)
        {
            var activity = await _repository.GetActivityAsync(activityId, trackChanges: false);
            if (activity == null)
            {
                return null;
            }
            var activityDto = _mapper.Map<ActivityForReadDto>(activity);
            return activityDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateActivityAsync(Guid activityId, JsonPatchDocument<ActivityForUpdateDto> patchDoc)
        {
            var activity = await _repository.GetActivityAsync(activityId, trackChanges: true);
            if (activity == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Activity with id: {activityId} doesn't exist in the database" };
            }
            var activityToPatch = _mapper.Map<ActivityForUpdateDto>(activity);
            patchDoc.ApplyTo(activityToPatch);
            _mapper.Map(activityToPatch, activity);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateActivityAsync(Guid activityId, ActivityForUpdateDto activityDto)
        {
            var activity = await _repository.GetActivityAsync(activityId, trackChanges: true);
            if (activity == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Activity with id: {activityId} doesn't exist in the database" };
            }
            _mapper.Map(activityDto, activity);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<IEnumerable<DayForChartDto>> GetDataForChartAsync(Guid id, int countDays)
        {
            var activities = await _repository.GetAllActivitiesForUserPerDays(id, countDays, trackChanges: false);
            var resultActivities = activities
                .GroupBy(x => x.Moment.Date)
                .Select(x =>
                    new DayForChartDto { Day = x.Key, CurrentCalories = x.Sum(k => k.TotalCaloriesSpent) });

            var result = new List<DayForChartDto>();
            for (int i = 0; i < countDays; i++)
            {
                var curDate = DateTime.Now.Date - new TimeSpan(24 * i, 0, 0);
                var resultAdd = new DayForChartDto { Day = curDate, CurrentCalories = 0 };
                var tempAct = resultActivities.SingleOrDefault(x => x.Day == curDate);
                if (tempAct != null)
                    resultAdd.CurrentCalories += tempAct.CurrentCalories;
                result.Add(resultAdd);
            }

            return result.OrderBy(x => x.Day);
        }
    }
}
