using ActivityMicroService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActivityMicroService.Contracts
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAllActivitiesForUserForDateAsync(Guid userId, DateTime dateTime, bool trackChanges);
        Task<IEnumerable<Activity>> GetAllActivitiesForUserPerDays(Guid userId, int days, bool trackChanges);
        Task<Activity> GetActivityAsync(Guid activityId, bool trackChanges);
        void CreateActivity(Activity activity);
        void DeleteActivity(Activity activity);
        Task SaveAsync();
    }
}
