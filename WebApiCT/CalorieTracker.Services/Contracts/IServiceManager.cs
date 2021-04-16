namespace CaloriesTracker.Services.Interfaces
{
    public interface IServiceManager
    {
        IUserService User { get; }
        IActivityService Activity { get; }
        IEatingService Eating { get; }
        IExerciseService Exercise { get; }
        IIngredientService Ingredient { get; }
        IRecipeService Recipe { get; }
        IAuthenticationService Authentication { get; }
    }
}
