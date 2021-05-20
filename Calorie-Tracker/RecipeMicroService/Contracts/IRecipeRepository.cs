using RecipeMicroService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMicroService.Contracts
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipesForUserPaginationAsync(Guid userId, int pageSize, int number, bool trackChanges);
        Task<int> CountOfRecipesAsync(Guid userId, bool trackChanges);
        Task<Recipe> GetRecipeAsync(Guid recipeId, bool trackChanges);
        void CreateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
        Task SaveAsync();
    }
}
