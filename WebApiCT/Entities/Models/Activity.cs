﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaloriesTracker.Entities.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float TotalCaloriesSpent { get; set; }
        public virtual IEnumerable<ActivityExercise> ExercisesWithReps { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public Activity()
        {
            ExercisesWithReps = new List<ActivityExercise>();
        }
    }
}
