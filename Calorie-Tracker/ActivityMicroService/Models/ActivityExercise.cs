using System;

namespace ActivityMicroService.Models
{
    public class ActivityExercise
    {
        public Guid Id { get; set; }
        public Guid ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float CaloriesSpent { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfSets { get; set; }
    }
}
