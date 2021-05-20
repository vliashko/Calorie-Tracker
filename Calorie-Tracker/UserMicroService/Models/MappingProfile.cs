using AutoMapper;
using UserMicroService.DataTransferObjects;

namespace UserMicroService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfile, UserProfileForReadDto>();
            CreateMap<UserProfileForCreateDto, UserProfile>();
            CreateMap<UserProfileForUpdateDto, UserProfile>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, UserForReadDto>();
        }
    }
}
