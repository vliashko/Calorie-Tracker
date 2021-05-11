using CaloriesTracker.Entities.DataTransferObjects;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseForReadDto>> GetExercisesPaginationAsync(int pageSize, int number, string searchName);
        Task<int> GetExercisesCount(string searchName);
        Task<ExerciseForReadDto> GetExerciseAsync(Guid id);
        Task<ExerciseForReadDto> CreateExerciseAsync(ExerciseForCreateDto exerciseCreateDto);
        Task<MessageDetailsDto> DeleteExerciseAsync(Guid id);
        Task<MessageDetailsDto> UpdateExerciseAsync(Guid id, ExerciseForUpdateDto exerciseUpdateDto);
        Task<MessageDetailsDto> PartiallyUpdateExerciseAsync(Guid id, JsonPatchDocument<ExerciseForUpdateDto> exerciseUpdateDto);
    }
}
