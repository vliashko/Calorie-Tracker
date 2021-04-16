using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class ActivityForCreateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Start is required field.")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Finish is required field.")]
        public DateTime Finish { get; set; }

        public IEnumerable<ActivityExerciseForCreateDto> ExercisesWithReps { get; set; }

        public ActivityForCreateDto()
        {
            ExercisesWithReps = new List<ActivityExerciseForCreateDto>();
        }
    }
}
