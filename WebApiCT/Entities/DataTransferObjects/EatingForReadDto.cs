using System;
using System.Collections.Generic;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class EatingForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Moment { get; set; }

        public IEnumerable<IngredientEatingForReadDto> IngredientsWithGrams { get; set; }

        public EatingForReadDto()
        {
            IngredientsWithGrams = new List<IngredientEatingForReadDto>();
        }
    }
}
