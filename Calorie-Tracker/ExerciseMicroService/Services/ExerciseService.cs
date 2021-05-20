using AutoMapper;
using ExerciseMicroService.Contracts;
using ExerciseMicroService.DataTransferObjects;
using ExerciseMicroService.Models;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseMicroService.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;

        public ExerciseService(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExerciseForReadDto> CreateExerciseAsync(ExerciseForCreateDto exerciseCreateDto)
        {
            var exerciseEntity = _mapper.Map<Exercise>(exerciseCreateDto);
            _repository.CreateExercise(exerciseEntity);
            await _repository.SaveAsync();
            return _mapper.Map<ExerciseForReadDto>(exerciseEntity);
        }

        public async Task<MessageDetailsDto> DeleteExerciseAsync(Guid id)
        {
            var exercise = await _repository.GetExerciseAsync(id, trackChanges: false);
            if (exercise == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Exercise with id: {id} doesn't exist in the database" };
            }
            _repository.DeleteExercise(exercise);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<ExerciseForReadDto> GetExerciseAsync(Guid id)
        {
            var exercise = await _repository.GetExerciseAsync(id, trackChanges: false);
            if (exercise == null)
            {
                return null;
            }
            var exerciseDto = _mapper.Map<ExerciseForReadDto>(exercise);
            return exerciseDto;
        }

        public async Task<int> GetExercisesCount(string searchName)
        {
            return await _repository.CountOfExercisesAsync(searchName, false);
        }

        public async Task<IEnumerable<ExerciseForReadDto>> GetExercisesPaginationAsync(int pageSize, int number, string searchName)
        {
            var exercises = await _repository.GetAllExercisesPaginationAsync(pageSize, number, searchName, trackChanges: false);
            var exercisesDto = _mapper.Map<IEnumerable<ExerciseForReadDto>>(exercises);
            return exercisesDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateExerciseAsync(Guid id, JsonPatchDocument<ExerciseForUpdateDto> exerciseUpdateDto)
        {
            var exercise = await _repository.GetExerciseAsync(id, trackChanges: true);
            if (exercise == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Exercise with id: {id} doesn't exist in the database" };
            }
            var exerciseToPatch = _mapper.Map<ExerciseForUpdateDto>(exercise);
            exerciseUpdateDto.ApplyTo(exerciseToPatch);
            _mapper.Map(exerciseToPatch, exercise);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateExerciseAsync(Guid id, ExerciseForUpdateDto exerciseUpdateDto)
        {
            var exercise = await _repository.GetExerciseAsync(id, trackChanges: true);
            if (exercise == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Exercise with id: {id} doesn't exist in the database" };
            }
            _mapper.Map(exerciseUpdateDto, exercise);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
