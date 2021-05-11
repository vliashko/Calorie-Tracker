using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> userManager;

        public UserService(IRepositoryManager repositoryManager, 
                           ILoggerManager logger, 
                           IMapper mapper, 
                           UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<MessageDetailsDto> DeleteUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return new MessageDetailsDto { StatusCode = 404, Message = $"User with id: {id} doesn't exist in the database" };
            var userProfile = await _repositoryManager.User.GetUserProfileByUserIdAsync(id, false);
            if (userProfile == null)
                return new MessageDetailsDto { StatusCode = 404, Message = $"UserProfile with for user with id: {id} doesn't exist in the database" };
            _repositoryManager.User.DeleteUserProfile(userProfile);
            var delUser = await userManager.DeleteAsync(user);
            if (!delUser.Succeeded)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in delUser.Errors)
                {
                    message.AppendLine(item.Description);
                }
                return new MessageDetailsDto { StatusCode = 400, Message = message.ToString() };
            }
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<UserForReadDto> GetUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userDto = _mapper.Map<UserForReadDto>(user);
            var userProfile = await _repositoryManager.User.GetUserProfileByUserIdAsync(id, false);
            userDto.UserProfile = _mapper.Map<UserProfileForReadDto>(userProfile);
            return userDto;
        }

        public async Task<int> GetUsersCount(UserSearchModelDto userSearch)
        {
            return await _repositoryManager.User.CountOfUserProfilesAsync(userSearch, false);
        }

        public async Task<IEnumerable<UserForReadDto>> GetUsersPaginationAsync(int pageSize, int number, UserSearchModelDto userSearch)
        {
            var users = await _repositoryManager.User.GetAllUserProfilesPaginationAsync(pageSize, number, userSearch, trackChanges: false);
            var usersResult = new List<UserForReadDto>();
            foreach (var user in users)
            {
                var tmp = await userManager.FindByIdAsync(user.UserId);
                tmp.UserProfile = user;
                usersResult.Add(_mapper.Map<UserForReadDto>(tmp));
            }
            return usersResult;
        }

        public async Task<MessageDetailsDto> UpdateUserAsync(string id, UserForUpdateDto userUpdateDto)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return new MessageDetailsDto { StatusCode = 404, Message = $"User with id: {id} doesn't exist in the database" };
            user.Email = userUpdateDto.Email;
            user.UserName = userUpdateDto.UserName;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    message.Append(" ");
                    message.AppendLine(item.Description);
                }
                return new MessageDetailsDto { StatusCode = 400, Message = message.ToString() };
            }
            var userProfile = await _repositoryManager.User.GetUserProfileByUserIdAsync(id, true);
            if (userProfile == null)
                return new MessageDetailsDto { StatusCode = 404, Message = $"UserProfile with for user with id: {id} doesn't exist in the database" };
            _mapper.Map(userUpdateDto.UserProfile, userProfile);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
