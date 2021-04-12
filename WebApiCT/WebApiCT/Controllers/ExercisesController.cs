using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiCT.Controllers
{
    [Route("api/exercises")]
    [ApiController]
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
            var exercisesDto = mapper.Map<IEnumerable<Exercise>>(exercises);
            return Ok(exercisesDto);
        }
        [HttpGet("{id}")]
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
    }
}
