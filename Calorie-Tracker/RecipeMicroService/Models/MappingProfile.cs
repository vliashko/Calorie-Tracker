using AutoMapper;
using RecipeMicroService.DataTransferObjects;

namespace RecipeMicroService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, RecipeForReadDto>();
            CreateMap<RecipeForCreateDto, Recipe>();
            CreateMap<RecipeForUpdateDto, Recipe>().ReverseMap();

            CreateMap<IngredientRecipe, IngredientRecipeForReadDto>();
            CreateMap<IngredientRecipeForCreateDto, IngredientRecipe>();
            CreateMap<IngredientRecipeForUpdateDto, IngredientRecipe>().ReverseMap();
        }
    }
}
