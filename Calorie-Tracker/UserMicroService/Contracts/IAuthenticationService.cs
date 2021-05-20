using System.Threading.Tasks;
using UserMicroService.DataTransferObjects;

namespace UserMicroService.Contracts
{
    public interface IAuthenticationService
    {
        Task<MessageDetailsDto> RegisterUser(UserForRegistrationDto userDto);
        Task<MessageDetailsDto> Authenticate(UserForAuthenticationDto userDto);
        Task<MessageDetailsDto> GenerateNewPassword(string id);
    }
}
