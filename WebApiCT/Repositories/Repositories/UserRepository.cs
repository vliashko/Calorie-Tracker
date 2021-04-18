using CaloriesTracker.Contracts;
using CaloriesTracker.Entities;
using CaloriesTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesTracker.Repositories
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
                .Include(user => user.Activities)
                    .ThenInclude(a => a.ExercisesWithReps)
                        .ThenInclude(er => er.Exercise)
                .Include(user => user.Eatings)
                    .ThenInclude(e => e.IngredientsWithGrams)
                        .ThenInclude(ig => ig.Ingredient)
                .Include(user => user.Recipes)
                .ToListAsync();

        public async Task<UserProfile> GetUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(user => user.Id.Equals(userId), trackChanges)
                .Include(user => user.Activities)
                .Include(user => user.Eatings)
                .Include(user => user.Recipes)
                .SingleOrDefaultAsync();
    }
}
