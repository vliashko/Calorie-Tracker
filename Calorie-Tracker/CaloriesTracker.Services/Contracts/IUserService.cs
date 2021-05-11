using CaloriesTracker.Entities.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserForReadDto>> GetUsersPaginationAsync(int pageSize, int number, UserSearchModelDto userSearch);
        Task<int> GetUsersCount(UserSearchModelDto userSearch);
        Task<UserForReadDto> GetUserAsync(string id);
        Task<MessageDetailsDto> DeleteUserAsync(string id);
        Task<MessageDetailsDto> UpdateUserAsync(string id, UserForUpdateDto userUpdateDto);
    }
}
