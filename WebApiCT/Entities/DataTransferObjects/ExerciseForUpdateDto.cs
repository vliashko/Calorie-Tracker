using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ExerciseForUpdateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required field.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "CaloriesSpent is required field.")]
        public double CaloriesSpent { get; set; }
    }
}
