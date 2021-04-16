using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityForReadDto>> GetActivities(Guid userId);
        Task<ActivityForReadDto> GetActivity(Guid userId, Guid activityId);
        Task<ActivityForReadDto> CreateActivity(Guid userId, ActivityForCreateDto activityDto);
        Task<bool> DeleteActivity(Guid userId, Guid activityId);
        Task<bool> UpdateActivity(Guid userId, Guid activityId, ActivityForUpdateDto activityDto);
        Task<bool> PartiallyUpdateActivity(Guid userId, Guid activityId, JsonPatchDocument<ActivityForUpdateDto> patchDoc);
    }
}
