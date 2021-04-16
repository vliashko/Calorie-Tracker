using System;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class IngredientRecipeForReadDto
    {
        public Guid IngredientId { get; set; }
        public float Grams { get; set; }
    }
}
