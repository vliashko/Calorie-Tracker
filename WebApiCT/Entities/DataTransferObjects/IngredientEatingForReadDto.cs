using System;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class IngredientEatingForReadDto
    {
        public Guid IngredientId { get; set; }
        public double Grams { get; set; }
    }
}
