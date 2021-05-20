using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserMicroService.DataTransferObjects;
using UserMicroService.Models;

namespace UserMicroService.Contracts
{
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UserProfile>> GetAllUserProfilesPaginationAsync(int pageSize, int number, UserSearchModelDto userSearch, bool trackChanges);
        Task<int> CountOfUserProfilesAsync(UserSearchModelDto userSearch, bool trackChanges);
        Task<UserProfile> GetUserProfileAsync(Guid userId, bool trackChanges);
        Task<UserProfile> GetUserProfileByUserIdAsync(string id, bool trackChanges);
        void CreateUserProfile(UserProfile user);
        void DeleteUserProfile(UserProfile user);
        Task SaveAsync();
    }
}
