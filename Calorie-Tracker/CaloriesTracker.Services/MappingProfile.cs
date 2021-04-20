using AutoMapper;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;

namespace CaloriesTracker.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfile, UserProfileForReadDto>();
            CreateMap<UserProfileForCreateDto, UserProfile>();
            CreateMap<UserProfileForUpdateDto, UserProfile>().ReverseMap();

            CreateMap<Activity, ActivityForReadDto>();
            CreateMap<ActivityForCreateDto, Activity>().ForMember(x => x.ExercisesWithReps, c => c.MapFrom(x => x.ExercisesWithReps));
            CreateMap<ActivityForUpdateDto, Activity>().ForMember(x => x.ExercisesWithReps, c => c.MapFrom(x => x.ExercisesWithReps)).ReverseMap();

            CreateMap<Eating, EatingForReadDto>();
            CreateMap<EatingForCreateDto, Eating>().ForMember(x => x.IngredientsWithGrams, c => c.MapFrom(x => x.IngredientsWithGrams));
            CreateMap<EatingForUpdateDto, Eating>().ForMember(x => x.IngredientsWithGrams, c => c.MapFrom(x => x.IngredientsWithGrams)).ReverseMap();

            CreateMap<Recipe, RecipeForReadDto>();
            CreateMap<RecipeForCreateDto, Recipe>().ForMember(x => x.IngredientsWithGrams, c => c.MapFrom(x => x.IngredientsWithGrams));
            CreateMap<RecipeForUpdateDto, Recipe>().ForMember(x => x.IngredientsWithGrams, c => c.MapFrom(x => x.IngredientsWithGrams)).ReverseMap();

            CreateMap<Exercise, ExerciseForReadDto>();
            CreateMap<ExerciseForCreateDto, Exercise>();
            CreateMap<ExerciseForUpdateDto, Exercise>().ReverseMap();

            CreateMap<Ingredient, IngredientForReadDto>();
            CreateMap<IngredientForCreateDto, Ingredient>();
            CreateMap<IngredientForUpdateDto, Ingredient>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<IngredientEating, IngredientEatingForReadDto>();
            CreateMap<IngredientEatingForCreateDto, IngredientEating>();
            CreateMap<IngredientEatingForUpdateDto, IngredientEating>().ReverseMap();

            CreateMap<ActivityExercise, ActivityExerciseForReadDto>();
            CreateMap<ActivityExerciseForCreateDto, ActivityExercise>();
            CreateMap<ActivityExerciseForUpdateDto, ActivityExercise>().ReverseMap();

            CreateMap<IngredientRecipe, IngredientRecipeForReadDto>();
            CreateMap<IngredientRecipeForCreateDto, IngredientRecipe>();
            CreateMap<IngredientRecipeForUpdateDto, IngredientRecipe>().ReverseMap();
        }
    }
}
