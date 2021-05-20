using System;
using System.Collections.Generic;

namespace ActivityMicroService.DataTransferObjects
{
    public class ActivityForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Moment { get; set; }
        public float TotalCaloriesSpent { get; set; }

        public IEnumerable<ActivityExerciseForReadDto> ExercisesWithReps { get; set; }

        public ActivityForReadDto()
        {
            ExercisesWithReps = new List<ActivityExerciseForReadDto>();
        }
    }
}
