using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IEatingService
    {
        Task<IEnumerable<EatingForReadDto>> GetEatings(Guid userId);
        Task<EatingForReadDto> GetEating(Guid userId, Guid eatingId);
        Task<EatingForReadDto> CreateEating(Guid userId, EatingForCreateDto eatingDto);
        Task<bool> DeleteEating(Guid userId, Guid eatingId);
        Task<bool> UpdateEating(Guid userId, Guid eatingId, EatingForUpdateDto eatingDto);
        Task<bool> PartiallyUpdateEating(Guid userId, Guid eatingId, JsonPatchDocument<EatingForUpdateDto> patchDoc);
    }
}
