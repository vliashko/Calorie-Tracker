using System;
using System.Collections.Generic;
using System.Linq;

namespace CaloriesTracker.Entities.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public float TotalCalories
        {
            get
            {
                var calor = IngredientsWithGrams.Sum(x => x.Ingredient.Calories * x.Grams / 100.0f);
                return calor;
            }
            set { }
        }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public virtual IEnumerable<IngredientRecipe> IngredientsWithGrams { get; set; }

        public Recipe()
        {
            IngredientsWithGrams = new List<IngredientRecipe>();
        }
    }
}
