﻿using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CaloriesSpent { get; set; }
        public virtual IEnumerable<ActivityExercise> ActivityExercise { get; set; }

        public Exercise()
        {
            ActivityExercise = new List<ActivityExercise>();
        }
    }
}
