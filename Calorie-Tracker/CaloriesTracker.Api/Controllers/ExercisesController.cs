using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CaloriesTracker.Api.Filter;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
using System.Linq;
using CaloriesTracker.Entities.Pagination;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/exercises")]
    [ApiController]
    [Authorize]
    public class ExercisesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ExercisesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet("page/{number}/size/{pageSize}/params")]
        public async Task<IActionResult> GetExercises(int pageSize = 5, int number = 1, string searchName = "")
        {
            var exercises = await serviceManager.Exercise.GetExercisesPaginationAsync(pageSize, number, searchName);
            var count = await serviceManager.Exercise.GetExercisesCount(searchName);
            PageViewModel page = new PageViewModel(count, number, pageSize);
            ViewModel<ExerciseForReadDto> exerViewModel = new ViewModel<ExerciseForReadDto> { PageViewModel = page, Objects = exercises };
            return Ok(exerViewModel);
        }
        [HttpGet("{id}", Name = "ExerciseById")]
        public async Task<IActionResult> GetExercise(Guid id)
        {
            var exercise = await serviceManager.Exercise.GetExerciseAsync(id);
            if (exercise == null)
                return NotFound();
            return Ok(exercise);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreateDto exerciseDto)
        {
            var exerciseReadDto = await serviceManager.Exercise.CreateExerciseAsync(exerciseDto);
            return CreatedAtRoute("ExerciseById", new { id = exerciseReadDto.Id }, exerciseReadDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var result = await serviceManager.Exercise.DeleteExerciseAsync(id);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateExercise(Guid id, [FromBody] ExerciseForUpdateDto exerciseDto)
        {
            var result = await serviceManager.Exercise.UpdateExerciseAsync(id, exerciseDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateExercise(Guid id, [FromBody] JsonPatchDocument<ExerciseForUpdateDto> patchDoc)
        {
            var result = await serviceManager.Exercise.PartiallyUpdateExerciseAsync(id, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
