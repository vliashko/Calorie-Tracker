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
    public class EatingRepository : RepositoryBase<Eating>, IEatingRepository
    {
        public EatingRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateEating(Eating eating) => Create(eating);

        public void DeleteEating(Eating eating) => Delete(eating);

        public async Task<IEnumerable<Eating>> GetAllEatingsForUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(eat => eat.UserProfileId == userId, trackChanges)
                .Include(eat => eat.IngredientsWithGrams)
                .OrderBy(eat => eat.Moment)
                .ToListAsync();

        public async Task<IEnumerable<Eating>> GetAllEatingsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .Include(eat => eat.IngredientsWithGrams)
                .OrderBy(eat => eat.Moment)
                .ToListAsync();

        public async Task<Eating> GetEatingAsync(Guid eatingId, bool trackChanges) =>
            await FindByCondition(eat => eat.Id.Equals(eatingId), trackChanges)
                .Include(eat => eat.IngredientsWithGrams)
                .SingleOrDefaultAsync();

        public async Task<Eating> GetEatingForUserAsync(Guid userId, Guid eatingId, bool trackChanges) =>
            await FindByCondition(eat => eat.Id.Equals(eatingId), trackChanges)
                .Include(eat => eat.IngredientsWithGrams)
                .Where(eat => eat.UserProfileId == userId)
                .SingleOrDefaultAsync();
    }
}
