using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public IngredientService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IngredientForReadDto> CreateIngredientAsync(IngredientForCreateDto ingredientCreateDto)
        {
            var ingredientEntity = _mapper.Map<Ingredient>(ingredientCreateDto);
            _repositoryManager.Ingredient.CreateIngredient(ingredientEntity);
            await _repositoryManager.SaveAsync();
            var ingredientView = _mapper.Map<IngredientForReadDto>(ingredientEntity);
            return ingredientView;
        }

        public async Task<MessageDetailsDto> DeleteIngredientAsync(Guid id)
        {
            var ingredient = await _repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: false);
            if (ingredient == null)
            {
                _logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Ingredient with id: {id} doesn't exist in the database" };
            }
            _repositoryManager.Ingredient.DeleteIngredient(ingredient);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<IngredientForReadDto> GetIngredientAsync(Guid id)
        {
            var ingredient = await _repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: false);
            if (ingredient == null)
            {
                _logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return null;
            }
            var ingredientDto = _mapper.Map<IngredientForReadDto>(ingredient);
            return ingredientDto;
        }

        public async Task<int> GetIngredientsCounts(string searchName)
        {
            return await _repositoryManager.Ingredient.CountOfIngredientsAsync(searchName, false);
        }

        public async Task<IEnumerable<IngredientForReadDto>> GetIngredientsPaginationAsync(int pageSize, int number, string searchName)
        {
            var ingredients = await _repositoryManager.Ingredient.GetAllIngredientsPaginationAsync(pageSize, number, searchName, trackChanges: false);
            var ingredientsDto = _mapper.Map<IEnumerable<IngredientForReadDto>>(ingredients);
            return ingredientsDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateIngredientAsync(Guid id, JsonPatchDocument<IngredientForUpdateDto> ingredientUpdateDto)
        {
            var ingredientEntity = await _repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: true);
            if (ingredientEntity == null)
            {
                _logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Ingredient with id: {id} doesn't exist in the database" };
            }
            var ingredientToPatch = _mapper.Map<IngredientForUpdateDto>(ingredientEntity);

            ingredientUpdateDto.ApplyTo(ingredientToPatch);
            _mapper.Map(ingredientToPatch, ingredientEntity);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateIngredientAsync(Guid id, IngredientForUpdateDto ingredientUpdateDto)
        {
            var ingredient = await _repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: true);
            if (ingredient == null)
            {
                _logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Ingredient with id: {id} doesn't exist in the database" };
            }
            _mapper.Map(ingredientUpdateDto, ingredient);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
