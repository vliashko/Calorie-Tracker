using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CaloriesTracker.Api.Filter;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
using CaloriesTracker.Entities.Pagination;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    [Authorize]
    public class IngredientsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public IngredientsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet("page/{number}/size/{pageSize}/params")]
        public async Task<IActionResult> GetIngredients(int pageSize = 5, int number = 1, string searchName = "")
        {
            var ingredients = await serviceManager.Ingredient.GetIngredientsPaginationAsync(pageSize, number, searchName);
            var count = await serviceManager.Ingredient.GetIngredientsCounts(searchName);
            PageViewModel page = new PageViewModel(count, number, pageSize);
            ViewModel<IngredientForReadDto> ingrViewModel = new ViewModel<IngredientForReadDto> { PageViewModel = page, Objects = ingredients };
            return Ok(ingrViewModel);
        }
        [HttpGet("{id}", Name = "IngredientById")]
        public async Task<IActionResult> GetIngredient(Guid id)
        {
            var ingredient = await serviceManager.Ingredient.GetIngredientAsync(id);
            if (ingredient == null)
                return NotFound();
            return Ok(ingredient);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientForCreateDto ingredientDto)
        {
            var ingredientView = await serviceManager.Ingredient.CreateIngredientAsync(ingredientDto);
            return CreatedAtRoute("IngredientById", new { id = ingredientView.Id }, ingredientView);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var result = await serviceManager.Ingredient.DeleteIngredientAsync(id);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] IngredientForUpdateDto ingredientDto)
        {
            var result = await serviceManager.Ingredient.UpdateIngredientAsync(id, ingredientDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateIngredient(Guid id, [FromBody] JsonPatchDocument<IngredientForUpdateDto> patchDoc)
        {
            var result = await serviceManager.Ingredient.PartiallyUpdateIngredientAsync(id, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
