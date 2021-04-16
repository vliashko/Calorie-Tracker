using System;
using System.Collections.Generic;

namespace CaloriesTracker.Entities.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public double TotalCaloriesSpent { get; set; }
        public virtual IEnumerable<ActivityExercise> ExercisesWithReps { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public Activity()
        {
            ExercisesWithReps = new List<ActivityExercise>();
        }
    }
}
