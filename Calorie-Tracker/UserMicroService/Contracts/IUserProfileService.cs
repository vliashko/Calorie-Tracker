using Marvin.JsonPatch;
using System;
using System.Threading.Tasks;
using UserMicroService.DataTransferObjects;

namespace UserMicroService.Contracts
{
    public interface IUserProfileService
    {
        Task<UserProfileForReadDto> GetUserProfileByUserIdAsync(string id);
        Task<UserProfileForReadDto> CreateUserProfileForUserAsync(string id, UserProfileForCreateDto userDto);
        Task<MessageDetailsDto> UpdateUserProfileAsync(Guid id, UserProfileForUpdateDto userDto);
        Task<MessageDetailsDto> PartiallyUpdateUserProfileAsync(Guid id, JsonPatchDocument<UserProfileForUpdateDto> patchDoc);
    }
}
