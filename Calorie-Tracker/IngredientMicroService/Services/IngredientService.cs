using AutoMapper;
using IngredientMicroService.Contracts;
using IngredientMicroService.DataTransferObjects;
using IngredientMicroService.Models;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IngredientMicroService.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<IngredientForReadDto> CreateIngredientAsync(IngredientForCreateDto ingredientCreateDto)
        {
            var ingredientEntity = _mapper.Map<Ingredient>(ingredientCreateDto);
            _ingredientRepository.CreateIngredient(ingredientEntity);
            await _ingredientRepository.SaveAsync();
            var ingredientView = _mapper.Map<IngredientForReadDto>(ingredientEntity);
            return ingredientView;
        }

        public async Task<MessageDetailsDto> DeleteIngredientAsync(Guid id)
        {
            var ingredient = await _ingredientRepository.GetIngredientAsync(id, trackChanges: false);
            if (ingredient == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Ingredient with id: {id} doesn't exist in the database" };
            }
            _ingredientRepository.DeleteIngredient(ingredient);
            await _ingredientRepository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<IngredientForReadDto> GetIngredientAsync(Guid id)
        {
            var ingredient = await _ingredientRepository.GetIngredientAsync(id, trackChanges: false);
            if (ingredient == null)
            {
                return null;
            }
            var ingredientDto = _mapper.Map<IngredientForReadDto>(ingredient);
            return ingredientDto;
        }

        public async Task<int> GetIngredientsCounts(string searchName)
        {
            return await _ingredientRepository.CountOfIngredientsAsync(searchName, false);
        }

        public async Task<IEnumerable<IngredientForReadDto>> GetIngredientsPaginationAsync(int pageSize, int number, string searchName)
        {
            var ingredients = await _ingredientRepository.GetAllIngredientsPaginationAsync(pageSize, number, searchName, trackChanges: false);
            var ingredientsDto = _mapper.Map<IEnumerable<IngredientForReadDto>>(ingredients);
            return ingredientsDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateIngredientAsync(Guid id, JsonPatchDocument<IngredientForUpdateDto> ingredientUpdateDto)
        {
            var ingredientEntity = await _ingredientRepository.GetIngredientAsync(id, trackChanges: true);
            if (ingredientEntity == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Ingredient with id: {id} doesn't exist in the database" };
            }
            var ingredientToPatch = _mapper.Map<IngredientForUpdateDto>(ingredientEntity);

            ingredientUpdateDto.ApplyTo(ingredientToPatch);
            _mapper.Map(ingredientToPatch, ingredientEntity);
            await _ingredientRepository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateIngredientAsync(Guid id, IngredientForUpdateDto ingredientUpdateDto)
        {
            var ingredient = await _ingredientRepository.GetIngredientAsync(id, trackChanges: true);
            if (ingredient == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Ingredient with id: {id} doesn't exist in the database" };
            }
            _mapper.Map(ingredientUpdateDto, ingredient);
            await _ingredientRepository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
