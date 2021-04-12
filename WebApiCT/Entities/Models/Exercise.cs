using System;

namespace Entities.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double CaloriesPerMinute { get; set; }
        public string Description { get; set; }
    }
}
