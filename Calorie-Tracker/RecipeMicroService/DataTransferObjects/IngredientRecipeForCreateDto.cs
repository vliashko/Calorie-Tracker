using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeMicroService.DataTransferObjects
{
    public class IngredientRecipeForCreateDto
    {
        public string Name { get; set; }
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        [Range(0.01, 10000, ErrorMessage = "Grams bust be betwewn 0.01 and 10000.0")]
        public float Grams { get; set; }
    }
}
