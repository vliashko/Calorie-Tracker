using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public double TotalCalories { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public virtual IEnumerable<IngredientRecipe> IngredientsWithGrams { get; set; }

        public Recipe()
        {
            IngredientsWithGrams = new List<IngredientRecipe>();
        }
    }
}
