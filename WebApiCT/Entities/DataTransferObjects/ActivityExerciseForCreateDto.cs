using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ActivityExerciseForCreateDto
    {
        [Required(ErrorMessage = "ExerciseId is required field.")]
        public Guid ExerciseId { get; set; }
        [Range(1, 200, ErrorMessage = "NumberOfRepetitions bust be betwewn 1 and 200")]
        public int NumberOfRepetitions { get; set; }
        [Range(1, 200, ErrorMessage = "NumberOfSets bust be betwewn 1 and 200")]
        public int NumberOfSets { get; set; }
    }
}
