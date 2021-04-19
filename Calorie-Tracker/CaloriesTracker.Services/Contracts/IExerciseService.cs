using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseForReadDto>> GetExercises();
        Task<ExerciseForReadDto> GetExercise(Guid id);
        Task<ExerciseForReadDto> CreateExercise(ExerciseForCreateDto exerciseCreateDto);
        Task<bool> DeleteExercise(Guid id);
        Task<bool> UpdateExercise(Guid id, ExerciseForUpdateDto exerciseUpdateDto);
        Task<bool> PartiallyUpdateExercise(Guid id, JsonPatchDocument<ExerciseForUpdateDto> exerciseUpdateDto);
    }
}
