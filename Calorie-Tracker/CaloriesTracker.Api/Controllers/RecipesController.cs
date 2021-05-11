using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CaloriesTracker.Api.Filter;
using Microsoft.AspNetCore.Authorization;
using Marvin.JsonPatch;
using CaloriesTracker.Services.Interfaces;
using CaloriesTracker.Entities.Pagination;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/users/{userId}/recipes")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public RecipesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("page/{number}/size/{pageSize}")]
        public async Task<IActionResult> GetRecipes(Guid userId, int pageSize = 5, int number = 1)
        {
            var recipes = await _serviceManager.Recipe.GetRecipesForUserProfilePaginationAsync(userId, pageSize, number);
            var count = await _serviceManager.Recipe.GetRecipesCount(userId);
            PageViewModel page = new PageViewModel(count, number, pageSize);
            ViewModel<RecipeForReadDto> ingrViewModel = new ViewModel<RecipeForReadDto> { PageViewModel = page, Objects = recipes };
            return Ok(ingrViewModel);
        }
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<IActionResult> GetRecipe(Guid recipeId)
        {
            var recipe = await _serviceManager.Recipe.GetRecipeAsync(recipeId);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRecipe(Guid userId, [FromBody] RecipeForCreateDto recipeDto)
        {
            var recipeView = await _serviceManager.Recipe.CreateRecipeForUserProfileAsync(userId, recipeDto);
            if (recipeView == null)
                return NotFound();
            return CreatedAtRoute("GetRecipe", new { userId, recipeId = recipeView.Id }, recipeView);
        }
        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(Guid recipeId)
        {
            var result = await _serviceManager.Recipe.DeleteRecipeAsync(recipeId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{recipeId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateRecipe(Guid recipeId, [FromBody] RecipeForUpdateDto recipeDto)
        {
            var result = await _serviceManager.Recipe.UpdateRecipeAsync(recipeId, recipeDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{recipeId}")]
        public async Task<IActionResult> PartiallyUpdateRecipe(Guid recipeId, [FromBody] JsonPatchDocument<RecipeForUpdateDto> patchDoc)
        {
            var result = await _serviceManager.Recipe.PartiallyUpdateRecipeAsync(recipeId, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
