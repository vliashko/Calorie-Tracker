using System;

namespace CaloriesTracker.Entities.Models
{
    public class IngredientRecipe
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public float Grams { get; set; }
    }
}
