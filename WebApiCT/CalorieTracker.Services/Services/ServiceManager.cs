using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Services.Interfaces;
using CalorieTracker.Services.Services;

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
        private IAuthenticationService authenticationService;

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

        public IActivityService Activity => throw new System.NotImplementedException();

        public IEatingService Eating => throw new System.NotImplementedException();

        public IExerciseService Exercise
        {
            get
            {
                if (exerciseService == null)
                    exerciseService = new ExerciseService(mapper, repositoryManager, logger);
                return exerciseService;
            }
        }

        public IIngredientService Ingredient => throw new System.NotImplementedException();

        public IRecipeService Recipe => throw new System.NotImplementedException();

        public IAuthenticationService Authentication => throw new System.NotImplementedException();
    }
}
