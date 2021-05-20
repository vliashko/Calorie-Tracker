using System.Threading.Tasks;
using UserMicroService.DataTransferObjects;

namespace UserMicroService.Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
