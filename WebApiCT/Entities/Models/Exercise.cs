using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double CaloriesPerMinute { get; set; }
        public string Description { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfSets { get; set; }
        public int RestBetweenSets { get; set; }
        public virtual ICollection<ActivityExercise> ActivityExercise { get; set; } = new List<ActivityExercise>();
    }
}
