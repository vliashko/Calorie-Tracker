using ExerciseMicroService.Contracts;
using ExerciseMicroService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseMicroService.Repositories
{
    public class ExerciseRepository : RepositoryBase<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(RepositoryDbContext context) : base(context)
        {
        }

        public async Task<int> CountOfExercisesAsync(string searchName, bool trackChanges) =>
            await FindByCondition(exer => string.IsNullOrWhiteSpace(searchName) || exer.Name.Contains(searchName), trackChanges).CountAsync();

        public void CreateExercise(Exercise exercise) => Create(exercise);

        public void DeleteExercise(Exercise exercise) => Delete(exercise);

        public async Task<IEnumerable<Exercise>> GetAllExercisesPaginationAsync(int pageSize, int number, string searchName, bool trackChanges) =>
            await FindByCondition(exer => string.IsNullOrWhiteSpace(searchName) || exer.Name.Contains(searchName), trackChanges)
                .Skip((number - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(exer => exer.Name)
                .ToListAsync();

        public async Task<Exercise> GetExerciseAsync(Guid exerciseId, bool trackChanges) =>
            await FindByCondition(exer => exer.Id.Equals(exerciseId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
