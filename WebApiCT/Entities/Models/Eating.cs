using System;
using System.Collections.Generic;

namespace CaloriesTracker.Entities.Models
{
    public class Eating
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Moment { get; set; }
        public double TotalCalories { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public virtual IEnumerable<IngredientEating> IngredientsWithGrams { get; set; }

        public Eating()
        {
            IngredientsWithGrams = new List<IngredientEating>();
        }
    }
}
