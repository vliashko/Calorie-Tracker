using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Contracts.Authentication;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly UserManager<User> userManager;

        public AuthenticationService(IRepositoryManager repositoryManager,
                           ILoggerManager logger,
                           IMapper mapper,
                           IAuthenticationManager authenticationManager,
                           UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            _authenticationManager = authenticationManager;
            this.userManager = userManager;
        }

        public async Task<MessageDetailsDto> Authenticate(UserForAuthenticationDto userDto)
        {
            if (!await _authenticationManager.ValidateUser(userDto))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Failed. Wrong Username or password.");
                return new MessageDetailsDto { StatusCode = 400, Message = "Wrong Username or Password." };
            }
            var token = await _authenticationManager.CreateToken();
            return new MessageDetailsDto { StatusCode = 200, Message = $"{token}" };
        }

        public async Task<MessageDetailsDto> GenerateNewPassword(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return new MessageDetailsDto { StatusCode = 404, Message = $"User with id {id} not found." };
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            int length = 15;
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, res.ToString());
            var update = await userManager.UpdateAsync(user);
            if (!update.Succeeded)
                return new MessageDetailsDto { StatusCode = 400, Message = $"{update.Errors}" };
            return new MessageDetailsDto { StatusCode = 200, Message = res.ToString() };
        }

        public async Task<MessageDetailsDto> RegisterUser(UserForRegistrationDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var result = await userManager.CreateAsync(user, userDto.Password);
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
            if (!string.IsNullOrWhiteSpace(userDto.Role))
            {
                await userManager.AddToRoleAsync(user, userDto.Role);
            }
            return new MessageDetailsDto { StatusCode = 201, Message = "Successfully registered." };
        }
    }
}
