using System;

namespace Entities.DataTransferObjects
{
    public class ActivityExerciseForReadDto
    {
        public Guid ExerciseId { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfSets { get; set; }
    }
}
