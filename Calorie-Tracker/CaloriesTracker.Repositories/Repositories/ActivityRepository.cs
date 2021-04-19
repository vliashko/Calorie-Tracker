using CaloriesTracker.Contracts;
using CaloriesTracker.Entities;
using CaloriesTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesTracker.Repositories
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
                .Include(activ => activ.ExercisesWithReps)
                    .ThenInclude(er => er.Exercise)
                .OrderBy(activ => activ.Start)
                .SingleOrDefaultAsync();

        public async Task<Activity> GetActivityForUserAsync(Guid userId, Guid activityId, bool trackChanges) =>
            await FindByCondition(activ => activ.Id.Equals(activityId), trackChanges)
                .Where(activ => activ.UserProfileId == userId)
                .Include(activ => activ.ExercisesWithReps)
                    .ThenInclude(er => er.Exercise)
                .OrderBy(activ => activ.Start)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .Include(activ => activ.ExercisesWithReps)
                    .ThenInclude(er => er.Exercise)
                .OrderBy(activ => activ.Start)
                .ToListAsync();

        public async Task<IEnumerable<Activity>> GetAllActivitiesForUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(activ => activ.UserProfileId == userId, trackChanges)
                .Include(activ => activ.ExercisesWithReps)
                    .ThenInclude(er => er.Exercise)
                .OrderBy(activ => activ.Start)
                .ToListAsync();
    }
}
