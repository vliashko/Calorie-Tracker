using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAllActivitiesAsync(bool trackChanges);
        Task<IEnumerable<Activity>> GetAllActivitiesForUserAsync(Guid userId, bool trackChanges);
        Task<Activity> GetActivityAsync(Guid activityId, bool trackChanges);
        Task<Activity> GetActivityForUserAsync(Guid userId, Guid activityId, bool trackChanges);
        void CreateActivity(Activity activity);
        void DeleteActivity(Activity activity);
    }
}
