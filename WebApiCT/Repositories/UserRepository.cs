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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateUser(User user) => Create(user);

        public void DeleteUser(User user) => Delete(user);

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(user => user.Login).ToListAsync();

        public async Task<User> GetUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(user => user.Id.Equals(userId), trackChanges).SingleOrDefaultAsync();
    }
}
