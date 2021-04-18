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
    public class ActivityService : IActivityService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public ActivityService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ActivityForReadDto> CreateActivity(Guid userId, ActivityForCreateDto activityDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: true);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            foreach (var iteration in activityDto.ExercisesWithReps)
            {
                iteration.Exercise = await GetExerciseById(iteration.ExerciseId);
            }
            var activityEntity = mapper.Map<Activity>(activityDto);
            repositoryManager.Activity.CreateActivity(activityEntity);
            var activityView = mapper.Map<ActivityForReadDto>(activityEntity);
            user.Activities.Add(activityEntity);
            await repositoryManager.SaveAsync();
            return activityView;
        }

        public async Task<bool> DeleteActivity(Guid userId, Guid activityId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: false);
            if (activity == null)
            {
                logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return false;
            }
            repositoryManager.Activity.DeleteActivity(activity);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<ActivityForReadDto>> GetActivities(Guid userId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            var activities = await repositoryManager.Activity.GetAllActivitiesForUserAsync(userId, trackChanges: false);
            var activitiesDto = mapper.Map<IEnumerable<ActivityForReadDto>>(activities);
            return activitiesDto;
        }

        public async Task<ActivityForReadDto> GetActivity(Guid userId, Guid activityId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: false);
            if (activity == null)
            {
                logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return null;
            }
            var activityDto = mapper.Map<ActivityForReadDto>(activity);
            return activityDto;
        }

        public async Task<bool> PartiallyUpdateActivity(Guid userId, Guid activityId, JsonPatchDocument<ActivityForUpdateDto> patchDoc)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: true);
            if (activity == null)
            {
                logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return false;
            }
            var activityToPatch = mapper.Map<ActivityForUpdateDto>(activity);
            patchDoc.ApplyTo(activityToPatch);
            mapper.Map(activityToPatch, activity);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateActivity(Guid userId, Guid activityId, ActivityForUpdateDto activityDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: true);
            if (activity == null)
            {
                logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return false;
            }
            foreach (var iteration in activityDto.ExercisesWithReps)
            {
                iteration.Exercise = await GetExerciseById(iteration.ExerciseId);
            }
            mapper.Map(activityDto, activity);
            await repositoryManager.SaveAsync();
            return true;
        }
        private async Task<Exercise> GetExerciseById(Guid id)
        {
            return await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: true);
        }
    }
}
