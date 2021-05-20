using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Marvin.JsonPatch;
using ExerciseMicroService.Filter;
using ExerciseMicroService.DataTransferObjects;
using ExerciseMicroService.Models.Pagination;
using ExerciseMicroService.Contracts;

namespace ExerciseMicroService.Controllers
{
    [Route("api/exercises")]
    [ApiController]
    [Authorize]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _service;

        public ExercisesController(IExerciseService service)
        {
            _service = service;
        }
        [HttpGet("page/{number}/size/{pageSize}/params")]
        public async Task<IActionResult> GetExercises(int pageSize = 5, int number = 1, string searchName = "")
        {
            var exercises = await _service.GetExercisesPaginationAsync(pageSize, number, searchName);
            var count = await _service.GetExercisesCount(searchName);
            PageViewModel page = new PageViewModel(count, number, pageSize);
            ViewModel<ExerciseForReadDto> exerViewModel = new ViewModel<ExerciseForReadDto> { PageViewModel = page, Objects = exercises };
            return Ok(exerViewModel);
        }
        [HttpGet("{id}", Name = "ExerciseById")]
        public async Task<IActionResult> GetExercise(Guid id)
        {
            var exercise = await _service.GetExerciseAsync(id);
            if (exercise == null)
                return NotFound();
            return Ok(exercise);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreateDto exerciseDto)
        {
            var exerciseReadDto = await _service.CreateExerciseAsync(exerciseDto);
            return CreatedAtRoute("ExerciseById", new { id = exerciseReadDto.Id }, exerciseReadDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var result = await _service.DeleteExerciseAsync(id);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateExercise(Guid id, [FromBody] ExerciseForUpdateDto exerciseDto)
        {
            var result = await _service.UpdateExerciseAsync(id, exerciseDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PartiallyUpdateExercise(Guid id, [FromBody] JsonPatchDocument<ExerciseForUpdateDto> patchDoc)
        {
            var result = await _service.PartiallyUpdateExerciseAsync(id, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
