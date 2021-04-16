using System.ComponentModel.DataAnnotations;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class IngredientForUpdateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Range(0.01, 500.0, ErrorMessage = "Calories must be between 0.01 and 500.0")]
        public double Calories { get; set; }
        [Range(0.01, 150.0, ErrorMessage = "Calories must be between 0.01 and 150.0")]
        public double Proteins { get; set; }
        [Range(0.01, 150.0, ErrorMessage = "Calories must be between 0.01 and 150.0")]
        public double Fats { get; set; }
        [Range(0.01, 150.0, ErrorMessage = "Calories must be between 0.01 and 150.0")]
        public double Carbohydrates { get; set; }
    }
}
