using CaloriesTracker.Contracts;
using CaloriesTracker.Entities;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesTracker.Repositories
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(RepositoryDbContext context) : base(context)
        {
        }

        public async Task<int> CountOfUserProfilesAsync(UserSearchModelDto userSearch, bool trackChanges) => 
            await FindAll(trackChanges)
                .Include(user => user.User)
                .Where(user =>
                    (string.IsNullOrWhiteSpace(userSearch.UserName) ||
                        user.User.UserName.Contains(userSearch.UserName)) &&
                    (string.IsNullOrWhiteSpace(userSearch.Email) ||
                        user.User.Email.Contains(userSearch.Email)))
                .CountAsync();

        public void CreateUserProfile(UserProfile user) => Create(user);

        public void DeleteUserProfile(UserProfile user) => Delete(user);

        public async Task<IEnumerable<UserProfile>> GetAllUserProfilesPaginationAsync(int pageSize, int number, UserSearchModelDto userSearch, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(user => user.User)
                .Where(user =>
                    (string.IsNullOrWhiteSpace(userSearch.UserName) ||
                        user.User.UserName.Contains(userSearch.UserName)) &&
                    (string.IsNullOrWhiteSpace(userSearch.Email) ||
                        user.User.Email.Contains(userSearch.Email)))
                .Skip((number - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<UserProfile> GetUserProfileAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(user => user.Id.Equals(userId), trackChanges)
                .Include(user => user.Activities)
                    .ThenInclude(a => a.ExercisesWithReps)
                        .ThenInclude(er => er.Exercise)
                .Include(user => user.Eatings)
                    .ThenInclude(e => e.IngredientsWithGrams)
                        .ThenInclude(ig => ig.Ingredient)
                .Include(user => user.Recipes)
                    .ThenInclude(r => r.IngredientsWithGrams)
                        .ThenInclude(ig => ig.Ingredient)
                .SingleOrDefaultAsync();

        public async Task<UserProfile> GetUserProfileByUserIdAsync(string id, bool trackChanges) =>
            await FindByCondition(userPr => userPr.UserId == id, trackChanges)
                .Include(user => user.Activities)
                    .ThenInclude(a => a.ExercisesWithReps)
                        .ThenInclude(er => er.Exercise)
                .Include(user => user.Eatings)
                    .ThenInclude(e => e.IngredientsWithGrams)
                        .ThenInclude(ig => ig.Ingredient)
                .Include(user => user.Recipes)
                    .ThenInclude(r => r.IngredientsWithGrams)
                        .ThenInclude(ig => ig.Ingredient)
                .SingleOrDefaultAsync();
    }
}
