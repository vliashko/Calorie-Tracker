using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Contracts.Authentication;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CaloriesTracker.Services.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly UserManager<User> userManager;

        private IUserService _userService;
        private IActivityService _activityService;
        private IEatingService _eatingService;
        private IExerciseService _exerciseService;
        private IIngredientService _ingredientService;
        private IRecipeService _recipeService;
        private IUserProfileService _userProfileService;
        private IAuthenticationService _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, 
                              IMapper mapper, 
                              ILoggerManager logger, 
                              IAuthenticationManager authenticationManager,
                              UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
            _authenticationManager = authenticationManager;
            this.userManager = userManager;
        }

        public IUserService User
        {
            get
            {
                if (_userService == null)
                    _userService = new UserService(_repositoryManager, _logger, _mapper, userManager);
                return _userService;
            }
        }

        public IActivityService Activity
        {
            get
            {
                if (_activityService == null)
                    _activityService = new ActivityService(_repositoryManager, _logger, _mapper);
                return _activityService;
            }
        }

        public IEatingService Eating
        {
            get
            {
                if (_eatingService == null)
                    _eatingService = new EatingService(_repositoryManager, _logger, _mapper);
                return _eatingService;
            }
        }

        public IExerciseService Exercise
        {
            get
            {
                if (_exerciseService == null)
                    _exerciseService = new ExerciseService(_mapper, _repositoryManager, _logger);
                return _exerciseService;
            }
        }

        public IIngredientService Ingredient
        {
            get
            {
                if (_ingredientService == null)
                    _ingredientService = new IngredientService(_repositoryManager, _logger, _mapper);
                return _ingredientService;
            }
        }

        public IRecipeService Recipe
        {
            get
            {
                if (_recipeService == null)
                    _recipeService = new RecipeService(_repositoryManager, _logger, _mapper);
                return _recipeService;
            }
        }

        public IUserProfileService UserProfile
        {
            get
            {
                if (_userProfileService == null)
                    _userProfileService = new UserProfileService(_repositoryManager, _logger, _mapper);
                return _userProfileService;
            }
        }

        public IAuthenticationService Authentication
        {
            get
            {
                if (_authenticationService == null)
                    _authenticationService = new AuthenticationService(_repositoryManager, _logger, _mapper, _authenticationManager, userManager);
                return _authenticationService;
            }
        }
    }
}
