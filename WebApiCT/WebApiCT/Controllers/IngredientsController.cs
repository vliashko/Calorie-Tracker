using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCT.ActionFilter;

namespace WebApiCT.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class IngredientsController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public IngredientsController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.logger = logger;
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetIngredients()
        {
            var ingredients = await repositoryManager.Ingredient.GetAllIngredientsAsync(trackChanges: false);
            var ingredientsDto = mapper.Map<IEnumerable<IngredientForReadDto>>(ingredients);
            return Ok(ingredientsDto);
        }
        [HttpGet("{id}", Name = "IngredientById")]
        public async Task<IActionResult> GetIngredient(Guid id)
        {
            var ingredient = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: false);
            if(ingredient == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var ingredientDto = mapper.Map<IngredientForReadDto>(ingredient);
            return Ok(ingredientDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientForCreateDto ingredientDto)
        {
            var ingredientEntity = mapper.Map<Ingredient>(ingredientDto);
            repositoryManager.Ingredient.CreateIngredient(ingredientEntity);
            await repositoryManager.SaveAsync();
            var ingredientView = mapper.Map<IngredientForReadDto>(ingredientEntity);
            return CreatedAtRoute("IngredientById", new { id = ingredientView.Id }, ingredientView);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var ingredient = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: false);
            if (ingredient == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return NotFound();
            }
            repositoryManager.Ingredient.DeleteIngredient(ingredient);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] IngredientForUpdateDto ingredientDto)
        {
            var ingredient = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: true);
            if (ingredient == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return NotFound();
            }
            mapper.Map(ingredientDto, ingredient);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateIngredient(Guid id, [FromBody] JsonPatchDocument<IngredientForUpdateDto> patchDoc)
        {
            var ingredientEntity = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: true);
            if (ingredientEntity == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var ingredientToPatch = mapper.Map<IngredientForUpdateDto>(ingredientEntity);

            patchDoc.ApplyTo(ingredientToPatch, ModelState);
            TryValidateModel(ingredientToPatch);
            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            mapper.Map(ingredientToPatch, ingredientEntity);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
    }
}
