using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateActivity(Activity activity) => Create(activity);

        public void DeleteActivity(Activity activity) => Delete(activity);

        public async Task<Activity> GetActivityAsync(Guid activityId, bool trackChanges) =>
            await FindByCondition(activ => activ.Id.Equals(activityId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<Activity> GetActivityForUserAsync(Guid userId, Guid activityId, bool trackChanges) =>
            await FindByCondition(activ => activ.Id.Equals(activityId), trackChanges)
                .Where(activ => activ.ActivityUserProfile.SingleOrDefault(au => au.UserProfileId == userId) != null)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(activ => activ.Start)
                .ToListAsync();

        public async Task<IEnumerable<Activity>> GetAllActivitiesForUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(activ => activ.ActivityUserProfile.SingleOrDefault(au => au.UserProfileId == userId) != null, trackChanges)
                .OrderBy(activ => activ.Start)
                .ToListAsync();
    }
}
