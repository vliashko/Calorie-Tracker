using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync(bool trackChanges);
        Task<Recipe> GetRecipeAsync(Guid recipeId, bool trackChanges);
        void CreateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
    }
}
