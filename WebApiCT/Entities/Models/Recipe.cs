using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaloriesTracker.Entities.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float TotalCalories { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public virtual IEnumerable<IngredientRecipe> IngredientsWithGrams { get; set; }

        public Recipe()
        {
            IngredientsWithGrams = new List<IngredientRecipe>();
        }
    }
}
