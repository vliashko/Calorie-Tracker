using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using WebApiCT.ActionFilter;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace WebApiCT.Controllers
{
    [Route("api/users/{userId}/recipes")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public RecipesController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.logger = logger;
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetRecipes(Guid userId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var recipes = await repositoryManager.Recipe.GetAllRecipesForUserAsync(userId, trackChanges: false);
            var recipesDto = mapper.Map<IEnumerable<RecipeForReadDto>>(recipes);
            return Ok(recipesDto);
        }
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<IActionResult> GetRecipe(Guid userId, Guid recipeId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var recipe = await repositoryManager.Recipe.GetRecipeForUserAsync(userId, recipeId, trackChanges: false);
            if(recipe == null)
            {
                logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return NotFound();
            }
            var recipeDto = mapper.Map<RecipeForReadDto>(recipe);
            return Ok(recipeDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRecipe(Guid userId, [FromBody] RecipeForCreateDto recipeDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var recipe = mapper.Map<Recipe>(recipeDto);
            recipe.UserProfileId = userId;
            repositoryManager.Recipe.CreateRecipe(recipe);
            var recipeView = mapper.Map<RecipeForReadDto>(recipe);
            user.Recipes.Add(recipe);
            await repositoryManager.SaveAsync();
            return CreatedAtRoute("GetRecipe", new { userId, recipeId = recipeView.Id }, recipeView);
        }
        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(Guid userId, Guid recipeId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var recipe = await repositoryManager.Recipe.GetRecipeForUserAsync(userId, recipeId, trackChanges: false);
            if(recipe == null)
            {
                logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return NotFound();
            }
            repositoryManager.Recipe.DeleteRecipe(recipe);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPut("{recipeId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateRecipe(Guid userId, Guid recipeId, [FromBody] RecipeForUpdateDto recipeDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var recipe = await repositoryManager.Recipe.GetRecipeForUserAsync(userId, recipeId, trackChanges: true);
            if (recipe == null)
            {
                logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return NotFound();
            }
            mapper.Map(recipeDto, recipe);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{recipeId}")]
        public async Task<IActionResult> PartiallyUpdateRecipe(Guid userId, Guid recipeId, [FromBody] JsonPatchDocument<RecipeForUpdateDto> patchDoc)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var recipe = await repositoryManager.Recipe.GetRecipeForUserAsync(userId, recipeId, trackChanges: true);
            if (recipe == null)
            {
                logger.LogInfo($"Recipe with id: {recipeId} doesn't exist in the database");
                return NotFound();
            }
            var recipeToPatch = mapper.Map<RecipeForUpdateDto>(recipe);
            patchDoc.ApplyTo(recipeToPatch, ModelState);
            TryValidateModel(recipeToPatch);
            if(!ModelState.IsValid)
            {
                logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            mapper.Map(recipeToPatch, recipe);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
    }
}
