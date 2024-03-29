﻿using EatingMicroService.Contracts;
using EatingMicroService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatingMicroService.Repositories
{
    public class EatingRepository : RepositoryBase<Eating>, IEatingRepository
    {
        public EatingRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateEating(Eating eating)
        {
            Create(eating);
            context.IngredientEatings.AddRange(eating.IngredientsWithGrams);
        }

        public void DeleteEating(Eating eating) => Delete(eating);

        public async Task<IEnumerable<Eating>> GetAllEatingsForUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(eat => eat.UserProfileId == userId, trackChanges)
                .Include(eat => eat.IngredientsWithGrams)
                .OrderBy(eat => eat.Moment)
                .ToListAsync();

        public async Task<IEnumerable<Eating>> GetAllEatingsForUserForDateAsync(Guid userId,
                DateTime dateTime,
                bool trackChanges) =>
            await FindByCondition(eat =>
                    eat.UserProfileId == userId &&
                        eat.Moment.Date == dateTime.Date, trackChanges)
                .Include(eat => eat.IngredientsWithGrams)
                .OrderBy(eat => eat.Moment)
                .ToListAsync();

        public async Task<IEnumerable<Eating>> GetAllEatingsForUserPerDays(Guid userId, int days, bool trackChanges)
        {
            var date1 = DateTime.Now.Date.AddDays(-days);
            var date2 = DateTime.Now.Date;
            var res = await FindByCondition(eat => eat.UserProfileId == userId &&
                eat.Moment.Date >= date1 && eat.Moment.Date <= date2, trackChanges)
                .Include(eat => eat.IngredientsWithGrams)
                .OrderBy(eat => eat.Moment)
                .ToListAsync();
            return res;
        }

        public async Task<Eating> GetEatingAsync(Guid eatingId, bool trackChanges) =>
            await FindByCondition(eat => eat.Id.Equals(eatingId), trackChanges)
                .Include(eat => eat.IngredientsWithGrams)
                .SingleOrDefaultAsync();

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
