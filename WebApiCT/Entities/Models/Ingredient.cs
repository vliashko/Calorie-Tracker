using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public virtual IEnumerable<IngredientEating> IngredientEating { get; set; }
        public virtual IEnumerable<IngredientRecipe> IngredientRecipe { get; set; }

        public Ingredient()
        {
            IngredientEating = new List<IngredientEating>();
            IngredientRecipe = new List<IngredientRecipe>();
        }
    }
}
