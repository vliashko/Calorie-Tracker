using AutoMapper;
using ExerciseMicroService.DataTransferObjects;

namespace ExerciseMicroService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseForReadDto>();
            CreateMap<ExerciseForCreateDto, Exercise>();
            CreateMap<ExerciseForUpdateDto, Exercise>().ReverseMap();
        }
    }
}
