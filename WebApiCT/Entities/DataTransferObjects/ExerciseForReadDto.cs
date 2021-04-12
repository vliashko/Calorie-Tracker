using System;

namespace Entities.DataTransferObjects
{
    public class ExerciseForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double CaloriesPerMinute { get; set; }
        public string Description { get; set; }
    }
}
