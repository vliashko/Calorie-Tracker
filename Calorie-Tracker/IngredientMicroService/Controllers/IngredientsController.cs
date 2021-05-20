using IngredientMicroService.Contracts;
using IngredientMicroService.DataTransferObjects;
using IngredientMicroService.Filter;
using IngredientMicroService.Models.Pagination;
using Marvin.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IngredientMicroService.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    [Authorize]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _service;

        public IngredientsController(IIngredientService service)
        {
            _service = service;
        }
        [HttpGet("page/{number}/size/{pageSize}/params")]
        public async Task<IActionResult> GetIngredients(int pageSize = 5, int number = 1, string searchName = "")
        {
            var ingredients = await _service.GetIngredientsPaginationAsync(pageSize, number, searchName);
            var count = await _service.GetIngredientsCounts(searchName);
            PageViewModel page = new PageViewModel(count, number, pageSize);
            ViewModel<IngredientForReadDto> ingrViewModel = new ViewModel<IngredientForReadDto> { PageViewModel = page, Objects = ingredients };
            return Ok(ingrViewModel);
        }
        [HttpGet("{id}", Name = "IngredientById")]
        public async Task<IActionResult> GetIngredient(Guid id)
        {
            var ingredient = await _service.GetIngredientAsync(id);
            if (ingredient == null)
                return NotFound();
            return Ok(ingredient);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientForCreateDto ingredientDto)
        {
            var ingredientView = await _service.CreateIngredientAsync(ingredientDto);
            return CreatedAtRoute("IngredientById", new { id = ingredientView.Id }, ingredientView);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var result = await _service.DeleteIngredientAsync(id);
            return StatusCode(result.StatusCode, result.Message);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] IngredientForUpdateDto ingredientDto)
        {
            var result = await _service.UpdateIngredientAsync(id, ingredientDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateIngredient(Guid id, [FromBody] JsonPatchDocument<IngredientForUpdateDto> patchDoc)
        {
            var result = await _service.PartiallyUpdateIngredientAsync(id, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
