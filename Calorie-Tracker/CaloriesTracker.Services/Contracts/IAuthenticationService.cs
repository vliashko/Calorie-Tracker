using CaloriesTracker.Entities.DataTransferObjects;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<MessageDetailsDto> RegisterUser(UserForRegistrationDto userDto);
        Task<MessageDetailsDto> Authenticate(UserForAuthenticationDto userDto);
        Task<MessageDetailsDto> GenerateNewPassword(string id);
    }
}
