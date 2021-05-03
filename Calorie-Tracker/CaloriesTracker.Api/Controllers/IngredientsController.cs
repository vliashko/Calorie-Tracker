using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CaloriesTracker.Api.Filter;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class IngredientsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public IngredientsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetIngredients()
        {
            return Ok(await serviceManager.Ingredient.GetIngredients());
        }
        [HttpGet("{id}", Name = "IngredientById")]
        public async Task<IActionResult> GetIngredient(Guid id)
        {
            var ingredient = await serviceManager.Ingredient.GetIngredient(id);
            if (ingredient == null)
                return NotFound();
            return Ok(ingredient);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientForCreateDto ingredientDto)
        {
            var ingredientView = await serviceManager.Ingredient.CreateIngredient(ingredientDto);
            return CreatedAtRoute("IngredientById", new { id = ingredientView.Id }, ingredientView);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var result = await serviceManager.Ingredient.DeleteIngredient(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] IngredientForUpdateDto ingredientDto)
        {
            var result = await serviceManager.Ingredient.UpdateIngredient(id, ingredientDto);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateIngredient(Guid id, [FromBody] JsonPatchDocument<IngredientForUpdateDto> patchDoc)
        {
            var result = await serviceManager.Ingredient.PartiallyUpdateIngredient(id, patchDoc);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
