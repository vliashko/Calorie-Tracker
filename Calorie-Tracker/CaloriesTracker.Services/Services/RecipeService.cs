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
    public class RecipeService : IRecipeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RecipeService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RecipeForReadDto> CreateRecipeForUserProfileAsync(Guid id, RecipeForCreateDto recipeDto)
        {
            var user = await _repositoryManager.User.GetUserProfileAsync(id, trackChanges: true);
            if (user == null)
            {
                _logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return null;
            }
            foreach (var iteration in recipeDto.IngredientsWithGrams)
            {
                iteration.Ingredient = await _repositoryManager.Ingredient.GetIngredientAsync(iteration.IngredientId, true);
            }
            var recipe = _mapper.Map<Recipe>(recipeDto);
            recipe.UserProfileId = id;
            _repositoryManager.Recipe.CreateRecipe(recipe);
            var recipeView = _mapper.Map<RecipeForReadDto>(recipe);
            user.Recipes.Add(recipe);
            await _repositoryManager.SaveAsync();
            return recipeView;
        }

        public async Task<MessageDetailsDto> DeleteRecipeAsync(Guid recipeId)
        {
            var recipe = await _repositoryManager.Recipe.GetRecipeAsync(recipeId, trackChanges: false);
            if (recipe == null)
            {
                _logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Recipe with id: {recipeId} doesn't exist in the database" };
            }
            _repositoryManager.Recipe.DeleteRecipe(recipe);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<RecipeForReadDto> GetRecipeAsync(Guid recipeId)
        {
            var recipe = await _repositoryManager.Recipe.GetRecipeAsync(recipeId, trackChanges: false);
            if (recipe == null)
            {
                _logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return null;
            }
            var recipeDto = _mapper.Map<RecipeForReadDto>(recipe);
            return recipeDto;
        }

        public async Task<int> GetRecipesCount(Guid id)
        {
            return await _repositoryManager.Recipe.CountOfRecipesAsync(id, false);
        }

        public async Task<IEnumerable<RecipeForReadDto>> GetRecipesForUserProfilePaginationAsync(Guid id, int pageSize, int number)
        {
            var user = await _repositoryManager.User.GetUserProfileAsync(id, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return null;
            }
            var recipes = await _repositoryManager.Recipe.GetAllRecipesForUserPaginationAsync(id, pageSize, number, trackChanges: false);
            var recipesDto = _mapper.Map<IEnumerable<RecipeForReadDto>>(recipes);
            return recipesDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateRecipeAsync(Guid recipeId, JsonPatchDocument<RecipeForUpdateDto> patchDoc)
        {
            var recipe = await _repositoryManager.Recipe.GetRecipeAsync(recipeId, trackChanges: true);
            if (recipe == null)
            {
                _logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Recipe with id: {recipeId} doesn't exist in the database" };
            }
            var recipeToPatch = _mapper.Map<RecipeForUpdateDto>(recipe);
            patchDoc.ApplyTo(recipeToPatch);
            _mapper.Map(recipeToPatch, recipe);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateRecipeAsync(Guid recipeId, RecipeForUpdateDto recipeDto)
        {
            var recipe = await _repositoryManager.Recipe.GetRecipeAsync(recipeId, trackChanges: true);
            if (recipe == null)
            {
                _logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Recipe with id: {recipeId} doesn't exist in the database" };
            }
            foreach (var iteration in recipeDto.IngredientsWithGrams)
            {
                iteration.Ingredient = await _repositoryManager.Ingredient.GetIngredientAsync(iteration.IngredientId, true);
            }
            _mapper.Map(recipeDto, recipe);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
