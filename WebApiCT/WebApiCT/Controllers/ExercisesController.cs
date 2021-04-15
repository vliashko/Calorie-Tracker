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
    [Route("api/exercises")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ExercisesController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public ExercisesController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.logger = logger;
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await repositoryManager.Exercise.GetAllExercisesAsync(trackChanges: false);
            var exercisesDto = mapper.Map<IEnumerable<ExerciseForReadDto>>(exercises);
            return Ok(exercisesDto);
        }
        [HttpGet("{id}", Name = "ExerciseById")]
        public async Task<IActionResult> GetExercise(Guid id)
        {
            var exercise = await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: false);
            if(exercise == null)
            {
                logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var exerciseDto = mapper.Map<ExerciseForReadDto>(exercise);
            return Ok(exerciseDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreateDto exerciseDto)
        {
            var exerciseEntity = mapper.Map<Exercise>(exerciseDto);
            repositoryManager.Exercise.CreateExercise(exerciseEntity);
            await repositoryManager.SaveAsync();
            var exerciseReadDto = mapper.Map<ExerciseForReadDto>(exerciseEntity);
            return CreatedAtRoute("ExerciseById", new { id = exerciseReadDto.Id }, exerciseReadDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var exercise = await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: false);
            if (exercise == null)
            {
                logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return NotFound();
            }
            repositoryManager.Exercise.DeleteExercise(exercise);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateExercise(Guid id, [FromBody] ExerciseForUpdateDto exerciseDto)
        {
            var exercise = await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: true);
            if (exercise == null)
            {
                logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return NotFound();
            }
            mapper.Map(exerciseDto, exercise);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateExercise(Guid id, [FromBody] JsonPatchDocument<ExerciseForUpdateDto> patchDoc)
        {
            var exercise = await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: true);
            if (exercise == null)
            {
                logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var exerciseToPatch = mapper.Map<ExerciseForUpdateDto>(exercise);
            patchDoc.ApplyTo(exerciseToPatch, ModelState);
            TryValidateModel(exerciseToPatch);
            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            mapper.Map(exerciseToPatch, exercise);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
    }
}
