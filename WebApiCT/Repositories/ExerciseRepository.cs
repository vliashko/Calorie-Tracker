using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class ExerciseRepository : RepositoryBase<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateExercise(Exercise exercise) => Create(exercise);

        public void DeleteExercise(Exercise exercise) => Delete(exercise);

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(exer => exer.Name).ToListAsync();

        public async Task<Exercise> GetExerciseAsync(Guid exerciseId, bool trackChanges) =>
            await FindByCondition(exer => exer.Id.Equals(exerciseId), trackChanges).SingleOrDefaultAsync();
    }
}
