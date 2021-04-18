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
    public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateRecipe(Recipe recipe) => Create(recipe);

        public void DeleteRecipe(Recipe recipe) => Delete(recipe);

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(rec => rec.Name)
                .Include(rec => rec.IngredientsWithGrams)
                .ThenInclude(ig => ig.Ingredient)
                .ToListAsync();

        public async Task<IEnumerable<Recipe>> GetAllRecipesForUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(rec => rec.UserProfileId.Equals(userId), trackChanges)
                .OrderBy(rec => rec.Name)
                .Include(rec => rec.IngredientsWithGrams)
                .ThenInclude(ig => ig.Ingredient)
                .ToListAsync();

        public async Task<Recipe> GetRecipeAsync(Guid recipeId, bool trackChanges) =>
            await FindByCondition(rec => rec.Id.Equals(recipeId), trackChanges)
                .Include(rec => rec.IngredientsWithGrams)
                .ThenInclude(ig => ig.Ingredient)
                .SingleOrDefaultAsync();

        public async Task<Recipe> GetRecipeForUserAsync(Guid userId, Guid recipeId, bool trackChanges) =>
            await FindByCondition(rec => rec.Id.Equals(recipeId), trackChanges)
                .Where(rec => rec.UserProfileId.Equals(userId))
                .Include(rec => rec.IngredientsWithGrams)
                .ThenInclude(ig => ig.Ingredient)
                .SingleOrDefaultAsync();
    }
}
