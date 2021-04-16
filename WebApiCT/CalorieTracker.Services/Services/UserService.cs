using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public UserService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<UserProfileForReadDto> CreateUser(UserProfileForCreateDto userCreateDto, string id)
        {
            var userEntity = mapper.Map<UserProfile>(userCreateDto);
            userEntity.UserId = id;
            repositoryManager.User.CreateUser(userEntity);
            await repositoryManager.SaveAsync();
            var userView = mapper.Map<UserProfileForReadDto>(userEntity);
            return userView;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await repositoryManager.User.GetUserAsync(id, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return false;
            }
            repositoryManager.User.DeleteUser(user);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<UserProfileForReadDto> GetUser(Guid id)
        {
            var user = await repositoryManager.User.GetUserAsync(id, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return null;
            }
            var userDto = mapper.Map<UserProfileForReadDto>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserProfileForReadDto>> GetUsers()
        {
            var users = await repositoryManager.User.GetAllUsersAsync(trackChanges: false);
            var usersDto = mapper.Map<IEnumerable<UserProfileForReadDto>>(users);
            return usersDto;
        }
        public async Task<bool> PartiallyUpdateUser(Guid id, JsonPatchDocument<UserProfileForUpdateDto> userUpdateDto)
        {
            var userEntity = await repositoryManager.User.GetUserAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return false;
            }
            var userToPatch = mapper.Map<UserProfileForUpdateDto>(userEntity);
            userUpdateDto.ApplyTo(userToPatch);        
            mapper.Map(userToPatch, userEntity);
            await repositoryManager.SaveAsync();
            return true;
        }
        public async Task<bool> UpdateUser(Guid id, UserProfileForUpdateDto userUpdateDto)
        {
            var userEntity = await repositoryManager.User.GetUserAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return false;
            }
            mapper.Map(userUpdateDto, userEntity);
            await repositoryManager.SaveAsync();
            return true;
        }
    }
}
