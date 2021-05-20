using AutoMapper;
using IngredientMicroService.DataTransferObjects;

namespace IngredientMicroService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ingredient, IngredientForReadDto>();
            CreateMap<IngredientForCreateDto, Ingredient>();
            CreateMap<IngredientForUpdateDto, Ingredient>().ReverseMap();
        }
    }
}
