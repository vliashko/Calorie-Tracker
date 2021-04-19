using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserProfileForReadDto>> GetUsers();
        Task<UserProfileForReadDto> GetUser(Guid id);
        Task<UserProfileForReadDto> CreateUser(UserProfileForCreateDto userCreateDto, string userName);
        Task<bool> DeleteUser(Guid id);
        Task<bool> UpdateUser(Guid id, UserProfileForUpdateDto userUpdateDto);
        Task<bool> PartiallyUpdateUser(Guid id, JsonPatchDocument<UserProfileForUpdateDto> userUpdateDto);
    }
}
