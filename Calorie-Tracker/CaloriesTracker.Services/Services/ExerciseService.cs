using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IMapper mapper;
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;

        public ExerciseService(IMapper mapper, IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            this.mapper = mapper;
            this.repositoryManager = repositoryManager;
            this.logger = logger;
        }

        public async Task<ExerciseForReadDto> CreateExercise(ExerciseForCreateDto exerciseCreateDto)
        {
            var exerciseEntity = mapper.Map<Exercise>(exerciseCreateDto);
            repositoryManager.Exercise.CreateExercise(exerciseEntity);
            await repositoryManager.SaveAsync();
            return mapper.Map<ExerciseForReadDto>(exerciseEntity);
        }

        public async Task<bool> DeleteExercise(Guid id)
        {
            var exercise = await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: false);
            if (exercise == null)
            {
                logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return false;
            }
            repositoryManager.Exercise.DeleteExercise(exercise);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<ExerciseForReadDto> GetExercise(Guid id)
        {
            var exercise = await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: false);
            if (exercise == null)
            {
                logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return null;
            }
            var exerciseDto = mapper.Map<ExerciseForReadDto>(exercise);
            return exerciseDto;
        }

        public async Task<IEnumerable<ExerciseForReadDto>> GetExercises()
        {
            var exercises = await repositoryManager.Exercise.GetAllExercisesAsync(trackChanges: false);
            var exercisesDto = mapper.Map<IEnumerable<ExerciseForReadDto>>(exercises);
            return exercisesDto;
        }

        public async Task<bool> PartiallyUpdateExercise(Guid id, JsonPatchDocument<ExerciseForUpdateDto> exerciseUpdateDto)
        {
            var exercise = await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: true);
            if (exercise == null)
            {
                logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return false;
            }
            var exerciseToPatch = mapper.Map<ExerciseForUpdateDto>(exercise);
            exerciseUpdateDto.ApplyTo(exerciseToPatch);
            mapper.Map(exerciseToPatch, exercise);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateExercise(Guid id, ExerciseForUpdateDto exerciseUpdateDto)
        {
            var exercise = await repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: true);
            if (exercise == null)
            {
                logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return false;
            }
            mapper.Map(exerciseUpdateDto, exercise);
            await repositoryManager.SaveAsync();
            return true;
        }
    }
}
