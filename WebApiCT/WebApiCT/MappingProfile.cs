using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApiCT
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForReadDto>();
            CreateMap<UserForCreateDto, User>();
            CreateMap<UserForUpdateDto, User>().ReverseMap();

            CreateMap<Activity, ActivityForReadDto>();
            CreateMap<ActivityForCreateDto, Activity>();
            CreateMap<ActivityForUpdateDto, Activity>().ReverseMap();

            CreateMap<Eating, EatingForReadDto>();
            CreateMap<EatingForCreateDto, Eating>();
            CreateMap<EatingForUpdateDto, Eating>().ReverseMap();

            CreateMap<Recipe, RecipeForReadDto>();
            CreateMap<RecipeForCreateDto, Recipe>();
            CreateMap<RecipeForUpdateDto, Recipe>().ReverseMap();
        }
    }
}
