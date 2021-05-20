using ActivityMicroService.DataTransferObjects;
using ActivityMicroService.Models;
using AutoMapper;

namespace IngredientMicroService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Activity, ActivityForReadDto>();
            CreateMap<ActivityForCreateDto, Activity>();
            CreateMap<ActivityForUpdateDto, Activity>().ReverseMap();

            CreateMap<ActivityExercise, ActivityExerciseForReadDto>();
            CreateMap<ActivityExerciseForCreateDto, ActivityExercise>();
            CreateMap<ActivityExerciseForUpdateDto, ActivityExercise>().ReverseMap();
        }
    }
}
