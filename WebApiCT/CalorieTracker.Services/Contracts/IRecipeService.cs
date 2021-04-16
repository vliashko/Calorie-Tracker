using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeForReadDto>> GetRecipes(Guid userId);
        Task<RecipeForReadDto> GetRecipe(Guid userId, Guid recipeId);
        Task<RecipeForReadDto> CreateRecipe(Guid userId, RecipeForCreateDto recipeDto);
        Task<bool> DeleteRecipe(Guid userId, Guid recipeId);
        Task<bool> UpdateRecipe(Guid userId, Guid recipeId, RecipeForUpdateDto recipeDto);
        Task<bool> PartiallyUpdateRecipe(Guid userId, Guid recipeId, JsonPatchDocument<RecipeForUpdateDto> patchDoc);
    }
}
