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
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public RecipeService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<RecipeForReadDto> CreateRecipe(Guid userId, RecipeForCreateDto recipeDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: true);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            foreach (var iteration in recipeDto.IngredientsWithGrams)
            {
                iteration.Ingredient = await GetIngredientById(iteration.IngredientId);
            }
            var recipe = mapper.Map<Recipe>(recipeDto);
            recipe.UserProfileId = userId;
            repositoryManager.Recipe.CreateRecipe(recipe);
            var recipeView = mapper.Map<RecipeForReadDto>(recipe);
            user.Recipes.Add(recipe);
            await repositoryManager.SaveAsync();
            return recipeView;
        }

        public async Task<bool> DeleteRecipe(Guid userId, Guid recipeId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var recipe = await repositoryManager.Recipe.GetRecipeForUserAsync(userId, recipeId, trackChanges: false);
            if (recipe == null)
            {
                logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return false;
            }
            repositoryManager.Recipe.DeleteRecipe(recipe);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<RecipeForReadDto> GetRecipe(Guid userId, Guid recipeId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            var recipe = await repositoryManager.Recipe.GetRecipeForUserAsync(userId, recipeId, trackChanges: false);
            if (recipe == null)
            {
                logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return null;
            }
            var recipeDto = mapper.Map<RecipeForReadDto>(recipe);
            return recipeDto;
        }

        public async Task<IEnumerable<RecipeForReadDto>> GetRecipes(Guid userId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            var recipes = await repositoryManager.Recipe.GetAllRecipesForUserAsync(userId, trackChanges: false);
            var recipesDto = mapper.Map<IEnumerable<RecipeForReadDto>>(recipes);
            return recipesDto;
        }

        public async Task<bool> PartiallyUpdateRecipe(Guid userId, Guid recipeId, JsonPatchDocument<RecipeForUpdateDto> patchDoc)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var recipe = await repositoryManager.Recipe.GetRecipeForUserAsync(userId, recipeId, trackChanges: true);
            if (recipe == null)
            {
                logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return false;
            }
            var recipeToPatch = mapper.Map<RecipeForUpdateDto>(recipe);
            patchDoc.ApplyTo(recipeToPatch);
            mapper.Map(recipeToPatch, recipe);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateRecipe(Guid userId, Guid recipeId, RecipeForUpdateDto recipeDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var recipe = await repositoryManager.Recipe.GetRecipeForUserAsync(userId, recipeId, trackChanges: true);
            if (recipe == null)
            {
                logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return false;
            }
            foreach (var iteration in recipeDto.IngredientsWithGrams)
            {
                iteration.Ingredient = await GetIngredientById(iteration.IngredientId);
            }
            mapper.Map(recipeDto, recipe);
            await repositoryManager.SaveAsync();
            return true;
        }
        private async Task<Ingredient> GetIngredientById(Guid id)
        {
            return await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: true);
        }
    }
}
