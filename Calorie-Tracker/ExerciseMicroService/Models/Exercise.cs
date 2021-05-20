using System;

namespace ExerciseMicroService.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float CaloriesSpent { get; set; }

        public Exercise()
        {
        }
    }
}
