using CaloriesTracker.Entities.DataTransferObjects;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileForReadDto> GetUserProfileByUserId(string userId);
    }
}
