using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Marvin.JsonPatch;
using RecipeMicroService.DataTransferObjects;
using RecipeMicroService.Contracts;
using RecipeMicroService.Models.Pagination;
using RecipeMicroService.Filter;

namespace RecipeMicroService.Controllers
{
    [Route("api/users/{userId}/recipes")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _service;

        public RecipesController(IRecipeService service)
        {
            _service = service;
        }
        [HttpGet("page/{number}/size/{pageSize}")]
        public async Task<IActionResult> GetRecipes(Guid userId, int pageSize = 5, int number = 1)
        {
            var recipes = await _service.GetRecipesForUserProfilePaginationAsync(userId, pageSize, number);
            var count = await _service.GetRecipesCount(userId);
            PageViewModel page = new PageViewModel(count, number, pageSize);
            ViewModel<RecipeForReadDto> ingrViewModel = new ViewModel<RecipeForReadDto> { PageViewModel = page, Objects = recipes };
            return Ok(ingrViewModel);
        }
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<IActionResult> GetRecipe(Guid recipeId)
        {
            var recipe = await _service.GetRecipeAsync(recipeId);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRecipe(Guid userId, [FromBody] RecipeForCreateDto recipeDto)
        {
            var recipeView = await _service.CreateRecipeForUserProfileAsync(userId, recipeDto);
            if (recipeView == null)
                return NotFound();
            return CreatedAtRoute("GetRecipe", new { userId, recipeId = recipeView.Id }, recipeView);
        }
        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(Guid recipeId)
        {
            var result = await _service.DeleteRecipeAsync(recipeId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{recipeId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateRecipe(Guid recipeId, [FromBody] RecipeForUpdateDto recipeDto)
        {
            var result = await _service.UpdateRecipeAsync(recipeId, recipeDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{recipeId}")]
        public async Task<IActionResult> PartiallyUpdateRecipe(Guid recipeId, [FromBody] JsonPatchDocument<RecipeForUpdateDto> patchDoc)
        {
            var result = await _service.PartiallyUpdateRecipeAsync(recipeId, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
