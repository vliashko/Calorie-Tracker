using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroService.Contracts;
using UserMicroService.DataTransferObjects;
using UserMicroService.Models;

namespace UserMicroService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserProfileRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> userManager;

        public UserService(IUserProfileRepository repository,
                           IMapper mapper,
                           UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<MessageDetailsDto> DeleteUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return new MessageDetailsDto { StatusCode = 404, Message = $"User with id: {id} doesn't exist in the database" };
            var userProfile = await _repository.GetUserProfileByUserIdAsync(id, false);
            if (userProfile == null)
                return new MessageDetailsDto { StatusCode = 404, Message = $"UserProfile with for user with id: {id} doesn't exist in the database" };
            _repository.DeleteUserProfile(userProfile);
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
            var userProfile = await _repository.GetUserProfileByUserIdAsync(id, false);
            userDto.UserProfile = _mapper.Map<UserProfileForReadDto>(userProfile);
            return userDto;
        }

        public async Task<int> GetUsersCount(UserSearchModelDto userSearch)
        {
            return await _repository.CountOfUserProfilesAsync(userSearch, false);
        }

        public async Task<IEnumerable<UserForReadDto>> GetUsersPaginationAsync(int pageSize, int number, UserSearchModelDto userSearch)
        {
            var users = await _repository.GetAllUserProfilesPaginationAsync(pageSize, number, userSearch, trackChanges: false);
            var usersResult = new List<UserForReadDto>();
            foreach (var user in users)
            {
                var tmp = await userManager.FindByIdAsync(user.UserId);
                tmp.UserProfile = user;
                usersResult.Add(_mapper.Map<UserForReadDto>(tmp));
            }
            return usersResult;
        }

        public async Task<IEnumerable<UserForReadDto>> GetUsersWithConfirmedEmail()
        {
            var users = await userManager.Users.Where(x => x.EmailConfirmed).ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<UserForReadDto>>(users);
            foreach (var user in usersDto)
            {
                user.UserProfileId = (await _repository.GetUserProfileByUserIdAsync(user.Id, false)).Id;
            }
            return _mapper.Map<IEnumerable<UserForReadDto>>(usersDto);
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
            var userProfile = await _repository.GetUserProfileByUserIdAsync(id, true);
            if (userProfile == null)
                return new MessageDetailsDto { StatusCode = 404, Message = $"UserProfile with for user with id: {id} doesn't exist in the database" };
            _mapper.Map(userUpdateDto.UserProfile, userProfile);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
