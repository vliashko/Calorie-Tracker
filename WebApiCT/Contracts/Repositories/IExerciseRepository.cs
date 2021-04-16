using CaloriesTracker.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Contracts
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllExercisesAsync(bool trackChanges);
        Task<Exercise> GetExerciseAsync(Guid exerciseId, bool trackChanges);
        void CreateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
    }
}
