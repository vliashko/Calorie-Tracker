using Microsoft.EntityFrameworkCore;
using RecipeMicroService.Contracts;
using RecipeMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeMicroService.Repositories
{
    public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RepositoryDbContext context) : base(context)
        {
        }

        public async Task<int> CountOfRecipesAsync(Guid id, bool trackChanges) =>
            await FindByCondition(rec => rec.UserProfileId.Equals(id), trackChanges)
                .CountAsync();

        public void CreateRecipe(Recipe recipe)
        {
            Create(recipe);
            context.IngredientRecipes.AddRange(recipe.IngredientsWithGrams);
        }

        public void DeleteRecipe(Recipe recipe) => Delete(recipe);

        public async Task<IEnumerable<Recipe>> GetAllRecipesForUserPaginationAsync(Guid userId, int pageSize, int number, bool trackChanges) =>
            await FindByCondition(rec => rec.UserProfileId.Equals(userId), trackChanges)
                .Skip((number - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(rec => rec.Name)
                .Include(rec => rec.IngredientsWithGrams)
                .ToListAsync();

        public async Task<Recipe> GetRecipeAsync(Guid recipeId, bool trackChanges) =>
            await FindByCondition(rec => rec.Id.Equals(recipeId), trackChanges)
                .Include(rec => rec.IngredientsWithGrams)
                .SingleOrDefaultAsync();

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
