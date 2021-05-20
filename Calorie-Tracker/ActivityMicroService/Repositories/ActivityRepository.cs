using ActivityMicroService.Contracts;
using ActivityMicroService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityMicroService.Repositories
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateActivity(Activity activity)
        {
            Create(activity);
            context.ActivityExercises.AddRange(activity.ExercisesWithReps);
        }

        public void DeleteActivity(Activity activity) => Delete(activity);

        public async Task<Activity> GetActivityAsync(Guid activityId, bool trackChanges) =>
            await FindByCondition(activ => activ.Id.Equals(activityId), trackChanges)
                .Include(activ => activ.ExercisesWithReps)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Activity>> GetAllActivitiesForUserForDateAsync(Guid userId,
                DateTime dateTime,
                bool trackChanges) =>
            await FindByCondition(activ =>
                    activ.UserProfileId == userId &&
                        activ.Moment.Date == dateTime.Date, trackChanges)
                .Include(activ => activ.ExercisesWithReps)
                .OrderBy(activ => activ.Moment)
                .ToListAsync();

        public async Task<IEnumerable<Activity>> GetAllActivitiesForUserPerDays(Guid userId, int days, bool trackChanges)
        {
            var date1 = DateTime.Now.Date.AddDays(-days);
            var date2 = DateTime.Now.Date;
            var res = await FindByCondition(act => act.UserProfileId == userId &&
                    act.Moment.Date >= date1 && act.Moment.Date <= date2, trackChanges)
                .Include(act => act.ExercisesWithReps)
                .OrderBy(act => act.Moment)
                .ToListAsync();
            return res;
        }

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
