using System;
using System.Collections.Generic;

namespace CaloriesTracker.Entities.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public virtual IEnumerable<IngredientEating> IngredientEating { get; set; }
        public virtual IEnumerable<IngredientRecipe> IngredientRecipe { get; set; }

        public Ingredient()
        {
            IngredientEating = new List<IngredientEating>();
            IngredientRecipe = new List<IngredientRecipe>();
        }
    }
}
