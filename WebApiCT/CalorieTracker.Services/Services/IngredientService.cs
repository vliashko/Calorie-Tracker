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
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public IngredientService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<IngredientForReadDto> CreateIngredient(IngredientForCreateDto ingredientCreateDto)
        {
            var ingredientEntity = mapper.Map<Ingredient>(ingredientCreateDto);
            repositoryManager.Ingredient.CreateIngredient(ingredientEntity);
            await repositoryManager.SaveAsync();
            var ingredientView = mapper.Map<IngredientForReadDto>(ingredientEntity);
            return ingredientView;
        }

        public async Task<bool> DeleteIngredient(Guid id)
        {
            var ingredient = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: false);
            if (ingredient == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return false;
            }
            repositoryManager.Ingredient.DeleteIngredient(ingredient);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<IngredientForReadDto> GetIngredient(Guid id)
        {
            var ingredient = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: false);
            if (ingredient == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return null;
            }
            var ingredientDto = mapper.Map<IngredientForReadDto>(ingredient);
            return ingredientDto;
        }

        public async Task<IEnumerable<IngredientForReadDto>> GetIngredients()
        {
            var ingredients = await repositoryManager.Ingredient.GetAllIngredientsAsync(trackChanges: false);
            var ingredientsDto = mapper.Map<IEnumerable<IngredientForReadDto>>(ingredients);
            return ingredientsDto;
        }

        public async Task<bool> PartiallyUpdateIngredient(Guid id, JsonPatchDocument<IngredientForUpdateDto> ingredientUpdateDto)
        {
            var ingredientEntity = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: true);
            if (ingredientEntity == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return false;
            }
            var ingredientToPatch = mapper.Map<IngredientForUpdateDto>(ingredientEntity);

            ingredientUpdateDto.ApplyTo(ingredientToPatch);
            mapper.Map(ingredientToPatch, ingredientEntity);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateIngredient(Guid id, IngredientForUpdateDto ingredientUpdateDto)
        {
            var ingredient = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: true);
            if (ingredient == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return false;
            }
            mapper.Map(ingredientUpdateDto, ingredient);
            await repositoryManager.SaveAsync();
            return true;
        }
    }
}
