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
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public ExerciseService(IMapper mapper, IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task<ExerciseForReadDto> CreateExerciseAsync(ExerciseForCreateDto exerciseCreateDto)
        {
            var exerciseEntity = _mapper.Map<Exercise>(exerciseCreateDto);
            _repositoryManager.Exercise.CreateExercise(exerciseEntity);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<ExerciseForReadDto>(exerciseEntity);
        }

        public async Task<MessageDetailsDto> DeleteExerciseAsync(Guid id)
        {
            var exercise = await _repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: false);
            if (exercise == null)
            {
                _logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Exercise with id: {id} doesn't exist in the database" };
            }
            _repositoryManager.Exercise.DeleteExercise(exercise);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<ExerciseForReadDto> GetExerciseAsync(Guid id)
        {
            var exercise = await _repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: false);
            if (exercise == null)
            {
                _logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return null;
            }
            var exerciseDto = _mapper.Map<ExerciseForReadDto>(exercise);
            return exerciseDto;
        }

        public async Task<int> GetExercisesCount(string searchName)
        {
            return await _repositoryManager.Exercise.CountOfExercisesAsync(searchName, false);
        }

        public async Task<IEnumerable<ExerciseForReadDto>> GetExercisesPaginationAsync(int pageSize, int number, string searchName)
        {
            var exercises = await _repositoryManager.Exercise.GetAllExercisesPaginationAsync(pageSize, number, searchName, trackChanges: false);
            var exercisesDto = _mapper.Map<IEnumerable<ExerciseForReadDto>>(exercises);
            return exercisesDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateExerciseAsync(Guid id, JsonPatchDocument<ExerciseForUpdateDto> exerciseUpdateDto)
        {
            var exercise = await _repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: true);
            if (exercise == null)
            {
                _logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Exercise with id: {id} doesn't exist in the database" };
            }
            var exerciseToPatch = _mapper.Map<ExerciseForUpdateDto>(exercise);
            exerciseUpdateDto.ApplyTo(exerciseToPatch);
            _mapper.Map(exerciseToPatch, exercise);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateExerciseAsync(Guid id, ExerciseForUpdateDto exerciseUpdateDto)
        {
            var exercise = await _repositoryManager.Exercise.GetExerciseAsync(id, trackChanges: true);
            if (exercise == null)
            {
                _logger.LogInfo($"Exercise with id: {id} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Exercise with id: {id} doesn't exist in the database" };
            }
            _mapper.Map(exerciseUpdateDto, exercise);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
