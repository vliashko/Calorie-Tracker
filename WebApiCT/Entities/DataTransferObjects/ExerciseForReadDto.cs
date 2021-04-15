using System;

namespace Entities.DataTransferObjects
{
    public class ExerciseForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CaloriesSpent { get; set; }
    }
}
