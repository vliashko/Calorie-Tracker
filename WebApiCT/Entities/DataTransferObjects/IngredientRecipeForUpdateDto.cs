using CaloriesTracker.Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class IngredientRecipeForUpdateDto
    {
        [Required(ErrorMessage = "IngredientId is required field.")]
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        [Range(0.01, 10000, ErrorMessage = "Grams bust be betwewn 0.01 and 10000.0")]
        public float Grams { get; set; }
    }
}
