using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityForReadDto>> GetActivitiesForUserProfileForDateAsync(Guid id);
        Task<IEnumerable<ActivityForReadDto>> GetActivitiesForUserProfilePerDaysAsync(Guid id, int days);
        Task<ActivityForReadDto> CreateActivityForUserProfileAsync(Guid id, ActivityForCreateDto activityDto);
        Task<ActivityForReadDto> GetActivityAsync(Guid activityId);
        Task<MessageDetailsDto> DeleteActivityAsync(Guid activityId);
        Task<MessageDetailsDto> UpdateActivityAsync(Guid activityId, ActivityForUpdateDto activityDto);
        Task<MessageDetailsDto> PartiallyUpdateActivityAsync(Guid activityId, JsonPatchDocument<ActivityForUpdateDto> patchDoc);
    }
}
