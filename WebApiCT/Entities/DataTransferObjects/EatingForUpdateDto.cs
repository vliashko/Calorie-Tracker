using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class EatingForUpdateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Moment is required field.")]
        public DateTime Moment { get; set; }

        public IEnumerable<IngredientEatingForUpdateDto> IngredientsWithGrams { get; set; }

        public EatingForUpdateDto()
        {
            IngredientsWithGrams = new List<IngredientEatingForUpdateDto>();
        }
    }
}
