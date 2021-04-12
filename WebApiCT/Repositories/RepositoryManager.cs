using Contracts;
using Entities;
using System;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryDbContext context;
        private IActivityRepository activityRepository;
        private IIngredientRepository ingredientRepository;
        private IExerciseRepository exerciseRepository;
        private UserRepository userRepository;

        public RepositoryManager(RepositoryDbContext context)
        {
            this.context = context;
        }

        public IUserRepository User
        {
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public IIngredientRepository Ingredient
        {
            get
            {
                if(ingredientRepository == null)
                {
                    ingredientRepository = new IngredientRepository(context);
                }
                return ingredientRepository;
            }
        }

        public IExerciseRepository Exercise
        {
            get
            {
                if(exerciseRepository == null)
                {
                    exerciseRepository = new ExerciseRepository(context);
                }
                return exerciseRepository;
            }
        }

        public IActivityRepository Activity
        {
            get
            {
                if(activityRepository == null)
                {
                    activityRepository = new ActivityRepository(context);
                }
                return activityRepository;
            }
        }

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
