using Marvin.JsonPatch;
using RecipeMicroService.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMicroService.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeForReadDto>> GetRecipesForUserProfilePaginationAsync(Guid id, int pageSize, int number);
        Task<int> GetRecipesCount(Guid userId);
        Task<RecipeForReadDto> GetRecipeAsync(Guid recipeId);
        Task<RecipeForReadDto> CreateRecipeForUserProfileAsync(Guid id, RecipeForCreateDto recipeDto);
        Task<MessageDetailsDto> DeleteRecipeAsync(Guid recipeId);
        Task<MessageDetailsDto> UpdateRecipeAsync(Guid recipeId, RecipeForUpdateDto recipeDto);
        Task<MessageDetailsDto> PartiallyUpdateRecipeAsync(Guid recipeId, JsonPatchDocument<RecipeForUpdateDto> patchDoc);
    }
}
