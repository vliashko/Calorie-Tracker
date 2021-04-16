using System;
using System.Collections.Generic;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class ActivityForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }

        public IEnumerable<ActivityExerciseForReadDto> ExercisesWithReps { get; set; }

        public ActivityForReadDto()
        {
            ExercisesWithReps = new List<ActivityExerciseForReadDto>();
        }
    }
}
