using CaloriesTracker.Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileForReadDto> GetUserProfileByUserId(string userId);
        Task<IEnumerable<DayForChartDto>> GetDataForChart(Guid userId, int countDays);
    }
}
