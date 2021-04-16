using System.ComponentModel.DataAnnotations;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class ExerciseForCreateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required field.")]
        public string Description { get; set; }
        [Range(0.01, 10.0, ErrorMessage = "CaloriesSpent must be between 0.01 and 10.0")]
        public double CaloriesSpent { get; set; }
    }
}
