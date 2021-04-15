using System;

namespace Entities.Models
{
    public class ActivityExercise
    {
        public Guid Id { get; set; }
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfSets { get; set; }
    }
}
