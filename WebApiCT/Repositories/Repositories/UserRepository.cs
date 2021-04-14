using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : RepositoryBase<UserProfile>, IUserRepository
    {
        public UserRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateUser(UserProfile user) => Create(user);

        public void DeleteUser(UserProfile user) => Delete(user);

        public async Task<IEnumerable<UserProfile>> GetAllUsersAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(user => user.Login)
                .ToListAsync();

        public async Task<UserProfile> GetUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(user => user.Id.Equals(userId), trackChanges)
                .SingleOrDefaultAsync();
    }
}
