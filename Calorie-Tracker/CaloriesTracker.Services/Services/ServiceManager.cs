using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Services.Interfaces;

namespace CaloriesTracker.Services.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        private IUserService userService;
        private IActivityService activityService;
        private IEatingService eatingService;
        private IExerciseService exerciseService;
        private IIngredientService ingredientService;
        private IRecipeService recipeService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.logger = logger;
        }

        public IUserService User
        {
            get
            {
                if (userService == null)
                    userService = new UserService(repositoryManager, logger, mapper);
                return userService;
            }
        }

        public IActivityService Activity
        {
            get
            {
                if (activityService == null)
                    activityService = new ActivityService(repositoryManager, logger, mapper);
                return activityService;
            }
        }

        public IEatingService Eating
        {
            get
            {
                if (eatingService == null)
                    eatingService = new EatingService(repositoryManager, logger, mapper);
                return eatingService;
            }
        }

        public IExerciseService Exercise
        {
            get
            {
                if (exerciseService == null)
                    exerciseService = new ExerciseService(mapper, repositoryManager, logger);
                return exerciseService;
            }
        }

        public IIngredientService Ingredient
        {
            get
            {
                if (ingredientService == null)
                    ingredientService = new IngredientService(repositoryManager, logger, mapper);
                return ingredientService;
            }
        }

        public IRecipeService Recipe
        {
            get
            {
                if (recipeService == null)
                    recipeService = new RecipeService(repositoryManager, logger, mapper);
                return recipeService;
            }
        }
    }
}
