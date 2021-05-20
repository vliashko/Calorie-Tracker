using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActivityMicroService.DataTransferObjects
{
    public class ActivityForCreateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Start is required field.")]
        public DateTime Moment { get; set; }

        public IEnumerable<ActivityExerciseForCreateDto> ExercisesWithReps { get; set; }

        public ActivityForCreateDto()
        {
            ExercisesWithReps = new List<ActivityExerciseForCreateDto>();
        }
    }
}
