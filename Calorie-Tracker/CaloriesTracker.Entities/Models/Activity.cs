using System;
using System.Collections.Generic;
using System.Linq;

namespace CaloriesTracker.Entities.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Moment { get; set; }
        public float TotalCaloriesSpent
        {
            get
            {
                var calor = ExercisesWithReps
                    .Sum(x => x.Exercise.CaloriesSpent * x.NumberOfRepetitions * x.NumberOfSets);
                return calor;
            }
            set { }
        }
        public virtual IEnumerable<ActivityExercise> ExercisesWithReps { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public Activity()
        {
            ExercisesWithReps = new List<ActivityExercise>();
        }
    }
}
