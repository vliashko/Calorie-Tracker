using AutoMapper;
using Marvin.JsonPatch;
using System;
using System.Threading.Tasks;
using UserMicroService.Contracts;
using UserMicroService.DataTransferObjects;
using UserMicroService.Models;

namespace UserMicroService.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _repository;
        private readonly IMapper mapper;

        public UserProfileService(IUserProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public async Task<UserProfileForReadDto> CreateUserProfileForUserAsync(string id, UserProfileForCreateDto userDto)
        {
            var userEntity = mapper.Map<UserProfile>(userDto);
            userEntity.UserId = id;
            _repository.CreateUserProfile(userEntity);
            await _repository.SaveAsync();
            var userView = mapper.Map<UserProfileForReadDto>(userEntity);
            return userView;
        }

        public async Task<UserProfileForReadDto> GetUserProfileByUserIdAsync(string id)
        {
            var user = await _repository.GetUserProfileByUserIdAsync(id, false);
            if (user == null)
                return null;
            var userDto = mapper.Map<UserProfileForReadDto>(user);
            return userDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateUserProfileAsync(Guid id, JsonPatchDocument<UserProfileForUpdateDto> patchDoc)
        {
            var userEntity = await _repository.GetUserProfileAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"UserProfile with id: {id} doesn't exist in the database" };
            }
            var userToPatch = mapper.Map<UserProfileForUpdateDto>(userEntity);
            patchDoc.ApplyTo(userToPatch);
            mapper.Map(userToPatch, userEntity);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateUserProfileAsync(Guid id, UserProfileForUpdateDto userDto)
        {
            var userEntity = await _repository.GetUserProfileAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"UserProfile with id: {id} doesn't exist in the database" };
            }
            mapper.Map(userDto, userEntity);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
