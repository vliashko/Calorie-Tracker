using CaloriesTracker.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Contracts
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync(bool trackChanges);
        Task<IEnumerable<Recipe>> GetAllRecipesForUserAsync(Guid userId, bool trackChanges);
        Task<Recipe> GetRecipeAsync(Guid recipeId, bool trackChanges);
        Task<Recipe> GetRecipeForUserAsync(Guid userId, Guid recipeId, bool trackChanges);
        void CreateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
    }
}
