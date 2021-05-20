using System;
using System.Collections.Generic;

namespace EatingMicroService.DataTransferObjects
{
    public class EatingForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Moment { get; set; }
        public float TotalCalories { get; set; }

        public IEnumerable<IngredientEatingForReadDto> IngredientsWithGrams { get; set; }

        public EatingForReadDto()
        {
            IngredientsWithGrams = new List<IngredientEatingForReadDto>();
        }
    }
}
