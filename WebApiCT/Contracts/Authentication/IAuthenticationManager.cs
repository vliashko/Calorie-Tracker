using CaloriesTracker.Entities.DataTransferObjects;
using System.Threading.Tasks;

namespace CaloriesTracker.Contracts.Authentication
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
