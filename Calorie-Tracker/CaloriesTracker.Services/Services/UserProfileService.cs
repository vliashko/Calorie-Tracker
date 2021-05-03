using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public UserProfileService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<UserProfileForReadDto> GetUserProfileByUserId(string userId)
        {
            var users = await repositoryManager.User.GetAllUsersAsync(false);
            var user = users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                return null;
            var userDto = mapper.Map<UserProfileForReadDto>(user);
            return userDto;
        }
    }
}
