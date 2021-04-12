using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiCT.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
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
            var ingredientsDto = mapper.Map<IEnumerable<Ingredient>>(ingredients);
            return Ok(ingredientsDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient(Guid id)
        {
            var ingredient = await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: false);
            if(ingredient == null)
            {
                logger.LogInfo($"Ingredient with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var ingredientDto = mapper.Map<Ingredient>(ingredient);
            return Ok(ingredientDto);
        }
    }
}
