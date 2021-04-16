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
    [Route("api/exercises")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ExercisesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ExercisesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            return Ok(await serviceManager.Exercise.GetExercises());
        }
        [HttpGet("{id}", Name = "ExerciseById")]
        public async Task<IActionResult> GetExercise(Guid id)
        {
            var exercise = await serviceManager.Exercise.GetExercise(id);
            if (exercise == null)
                return NotFound();
            return Ok(exercise);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreateDto exerciseDto)
        {
            var exerciseReadDto = await serviceManager.Exercise.CreateExercise(exerciseDto);
            return CreatedAtRoute("ExerciseById", new { id = exerciseReadDto.Id }, exerciseReadDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var result = await serviceManager.Exercise.DeleteExercise(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateExercise(Guid id, [FromBody] ExerciseForUpdateDto exerciseDto)
        {
            var result = await serviceManager.Exercise.UpdateExercise(id, exerciseDto);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateExercise(Guid id, [FromBody] JsonPatchDocument<ExerciseForUpdateDto> patchDoc)
        {
            var result = await serviceManager.Exercise.PartiallyUpdateExercise(id, patchDoc);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
