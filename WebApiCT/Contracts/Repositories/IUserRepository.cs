using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserProfile>> GetAllUsersAsync(bool trackChanges);
        Task<UserProfile> GetUserAsync(Guid userId, bool trackChanges);
        void CreateUser(UserProfile user);
        void DeleteUser(UserProfile user);
    }
}
