using CaloriesTracker.Contracts;
using CaloriesTracker.Entities;
using System.Threading.Tasks;

namespace CaloriesTracker.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryDbContext context;
        private IActivityRepository activityRepository;
        private IIngredientRepository ingredientRepository;
        private IExerciseRepository exerciseRepository;
        private IEatingRepository eatingRepository;
        private IUserProfileRepository userRepository;
        private IRecipeRepository recipeRepository;

        public RepositoryManager(RepositoryDbContext context)
        {
            this.context = context;
        }

        public IUserProfileRepository User
        {
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserProfileRepository(context);
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

        public IEatingRepository Eating
        {
            get
            {
                if(eatingRepository == null)
                {
                    eatingRepository = new EatingRepository(context);
                }
                return eatingRepository;
            }
        }

        public IRecipeRepository Recipe
        {
            get
            {
                if(recipeRepository == null)
                {
                    recipeRepository = new RecipeRepository(context);
                }
                return recipeRepository;
            }
        }

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
