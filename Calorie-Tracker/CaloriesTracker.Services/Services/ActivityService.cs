using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ActivityService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ActivityForReadDto> CreateActivityForUserProfileAsync(Guid id, ActivityForCreateDto activityDto)
        {
            var user = await _repositoryManager.User.GetUserProfileAsync(id, trackChanges: true);
            if (user == null)
            {
                _logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return null;
            }

            foreach (var iteration in activityDto.ExercisesWithReps)
            {
                iteration.Exercise = await _repositoryManager.Exercise.GetExerciseAsync(iteration.ExerciseId, true);
            }

            var activityEntity = _mapper.Map<Activity>(activityDto);
            _repositoryManager.Activity.CreateActivity(activityEntity);
            var activityView = _mapper.Map<ActivityForReadDto>(activityEntity);
            user.Activities.Add(activityEntity);
            await _repositoryManager.SaveAsync();
            return activityView;
        }

        public async Task<MessageDetailsDto> DeleteActivityAsync(Guid activityId)
        {
            var activity = await _repositoryManager.Activity.GetActivityAsync(activityId, trackChanges: false);
            if (activity == null)
            {
                _logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Activity with id: {activityId} doesn't exist in the database" };

            }
            _repositoryManager.Activity.DeleteActivity(activity);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<IEnumerable<ActivityForReadDto>> GetActivitiesForUserProfileForDateAsync(Guid id)
        {
            DateTime dateTime = DateTime.Now;
            var activities = await _repositoryManager.Activity.GetAllActivitiesForUserForDateAsync(id, dateTime, trackChanges: false);
            var activitiesDto = _mapper.Map<IEnumerable<ActivityForReadDto>>(activities);
            return activitiesDto;
        }

        public async Task<IEnumerable<ActivityForReadDto>> GetActivitiesForUserProfilePerDaysAsync(Guid id, int days)
        {
            var activities = await _repositoryManager.Activity.GetAllActivitiesForUserPerDays(id, days, trackChanges: false);
            var activitiesDto = _mapper.Map<IEnumerable<ActivityForReadDto>>(activities);
            return activitiesDto;
        }

        public async Task<ActivityForReadDto> GetActivityAsync(Guid activityId)
        {
            var activity = await _repositoryManager.Activity.GetActivityAsync(activityId, trackChanges: false);
            if (activity == null)
            {
                _logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return null;
            }
            var activityDto = _mapper.Map<ActivityForReadDto>(activity);
            return activityDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateActivityAsync(Guid activityId, JsonPatchDocument<ActivityForUpdateDto> patchDoc)
        {
            var activity = await _repositoryManager.Activity.GetActivityAsync(activityId, trackChanges: true);
            if (activity == null)
            {
                _logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Activity with id: {activityId} doesn't exist in the database" };
            }
            var activityToPatch = _mapper.Map<ActivityForUpdateDto>(activity);
            patchDoc.ApplyTo(activityToPatch);
            _mapper.Map(activityToPatch, activity);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateActivityAsync(Guid activityId, ActivityForUpdateDto activityDto)
        {
            var activity = await _repositoryManager.Activity.GetActivityAsync(activityId, trackChanges: true);
            if (activity == null)
            {
                _logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Activity with id: {activityId} doesn't exist in the database" };
            }
            foreach (var iteration in activityDto.ExercisesWithReps)
            {
                iteration.Exercise = await _repositoryManager.Exercise.GetExerciseAsync(iteration.ExerciseId, true);
            }
            _mapper.Map(activityDto, activity);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
