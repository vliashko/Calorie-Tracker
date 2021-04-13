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
            CreateMap<Activity, ActivityForReadDto>();
            CreateMap<Ingredient, IngredientForReadDto>();
            CreateMap<Exercise, ExerciseForReadDto>();
            CreateMap<UserForCreateDto, User>();
            CreateMap<UserForUpdateDto, User>().ReverseMap();
        }
    }
}
