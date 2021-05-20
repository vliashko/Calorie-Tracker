using AutoMapper;
using EatingMicroService.DataTransferObjects;

namespace EatingMicroService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Eating, EatingForReadDto>();
            CreateMap<EatingForCreateDto, Eating>();
            CreateMap<EatingForUpdateDto, Eating>().ReverseMap();

            CreateMap<IngredientEating, IngredientEatingForReadDto>();
            CreateMap<IngredientEatingForCreateDto, IngredientEating>();
            CreateMap<IngredientEatingForUpdateDto, IngredientEating>().ReverseMap();
        }
    }
}
