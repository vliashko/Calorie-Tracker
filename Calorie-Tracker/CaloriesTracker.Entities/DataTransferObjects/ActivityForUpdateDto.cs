using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class ActivityForUpdateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Start is required field.")]
        public DateTime Moment { get; set; }

        public IEnumerable<ActivityExerciseForUpdateDto> ExercisesWithReps { get; set; }

        public ActivityForUpdateDto()
        {
            ExercisesWithReps = new List<ActivityExerciseForUpdateDto>();
        }
    }
}
