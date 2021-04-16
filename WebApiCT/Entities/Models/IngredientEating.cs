using System;

namespace CaloriesTracker.Entities.Models
{
    public class IngredientEating
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public Guid EatingId { get; set; }
        public Eating Eating { get; set; }

        public double Grams { get; set; }
    }
}
