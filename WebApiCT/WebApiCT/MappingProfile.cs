using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApiCT
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfile, UserProfileForReadDto>();
            CreateMap<UserProfileForCreateDto, UserProfile>();
            CreateMap<UserProfileForUpdateDto, UserProfile>().ReverseMap();

            CreateMap<Activity, ActivityForReadDto>();
            CreateMap<ActivityForCreateDto, Activity>();
            CreateMap<ActivityForUpdateDto, Activity>().ReverseMap();

            CreateMap<Eating, EatingForReadDto>();
            CreateMap<EatingForCreateDto, Eating>();
            CreateMap<EatingForUpdateDto, Eating>().ReverseMap();

            CreateMap<Recipe, RecipeForReadDto>();
            CreateMap<RecipeForCreateDto, Recipe>();
            CreateMap<RecipeForUpdateDto, Recipe>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
