using ExerciseMicroService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseMicroService.Contracts
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllExercisesPaginationAsync(int pageSize, int number, string searchName, bool trackChanges);
        Task<int> CountOfExercisesAsync(string searchName, bool trackChanges);
        Task<Exercise> GetExerciseAsync(Guid exerciseId, bool trackChanges);
        void CreateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
        Task SaveAsync();
    }
}
