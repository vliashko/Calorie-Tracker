using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileForReadDto> GetUserProfileByUserIdAsync(string id);
        Task<UserProfileForReadDto> CreateUserProfileForUserAsync(string id, UserProfileForCreateDto userDto);
        Task<IEnumerable<DayForChartDto>> GetDataForChartAsync(Guid id, int countDays);
        Task<MessageDetailsDto> UpdateUserProfileAsync(Guid id, UserProfileForUpdateDto userDto);
        Task<MessageDetailsDto> PartiallyUpdateUserProfileAsync(Guid id, JsonPatchDocument<UserProfileForUpdateDto> patchDoc);
    }
}
