using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CaloriesTracker.Api.Filter;
using Microsoft.AspNetCore.Authorization;
using Marvin.JsonPatch;
using CaloriesTracker.Services.Interfaces;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/users/{userId}/recipes")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public RecipesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetRecipes(Guid userId)
        {
            var recipes = await serviceManager.Recipe.GetRecipes(userId);
            if (recipes == null)
                return NotFound();
            return Ok(recipes);
        }
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<IActionResult> GetRecipe(Guid userId, Guid recipeId)
        {
            var recipe = await serviceManager.Recipe.GetRecipe(userId, recipeId);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRecipe(Guid userId, [FromBody] RecipeForCreateDto recipeDto)
        {
            var recipeView = await serviceManager.Recipe.CreateRecipe(userId, recipeDto);
            if (recipeView == null)
                return NotFound();
            return CreatedAtRoute("GetRecipe", new { userId, recipeId = recipeView.Id }, recipeView);
        }
        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(Guid userId, Guid recipeId)
        {
            var result = await serviceManager.Recipe.DeleteRecipe(userId, recipeId);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPut("{recipeId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateRecipe(Guid userId, Guid recipeId, [FromBody] RecipeForUpdateDto recipeDto)
        {
            var result = await serviceManager.Recipe.UpdateRecipe(userId, recipeId, recipeDto);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch("{recipeId}")]
        public async Task<IActionResult> PartiallyUpdateRecipe(Guid userId, Guid recipeId, [FromBody] JsonPatchDocument<RecipeForUpdateDto> patchDoc)
        {
            var result = await serviceManager.Recipe.PartiallyUpdateRecipe(userId, recipeId, patchDoc);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
