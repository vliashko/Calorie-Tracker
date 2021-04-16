using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientForReadDto>> GetIngredients();
        Task<IngredientForReadDto> GetIngredient(Guid id);
        Task<IngredientForReadDto> CreateIngredient(IngredientForCreateDto ingredientCreateDto);
        Task<bool> DeleteIngredient(Guid id);
        Task<bool> UpdateIngredient(Guid id, IngredientForUpdateDto ingredientUpdateDto);
        Task<bool> PartiallyUpdateIngredient(Guid id, JsonPatchDocument<IngredientForUpdateDto> ingredientUpdateDto);
    }
}
