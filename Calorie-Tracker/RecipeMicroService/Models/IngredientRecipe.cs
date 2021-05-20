using System;

namespace RecipeMicroService.Models
{
    public class IngredientRecipe
    {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public float Grams { get; set; }
    }
}
