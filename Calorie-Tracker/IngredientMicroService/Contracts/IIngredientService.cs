using IngredientMicroService.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IngredientMicroService.Contracts
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientForReadDto>> GetIngredientsPaginationAsync(int pageSize, int number, string searchName);
        Task<int> GetIngredientsCounts(string searchName);
        Task<IngredientForReadDto> GetIngredientAsync(Guid id);
        Task<IngredientForReadDto> CreateIngredientAsync(IngredientForCreateDto ingredientCreateDto);
        Task<MessageDetailsDto> DeleteIngredientAsync(Guid id);
        Task<MessageDetailsDto> UpdateIngredientAsync(Guid id, IngredientForUpdateDto ingredientUpdateDto);
        Task<MessageDetailsDto> PartiallyUpdateIngredientAsync(Guid id, JsonPatchDocument<IngredientForUpdateDto> ingredientUpdateDto);
    }
}
