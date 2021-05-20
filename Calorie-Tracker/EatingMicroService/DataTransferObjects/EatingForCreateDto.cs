using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EatingMicroService.DataTransferObjects
{
    public class EatingForCreateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Moment is required field.")]
        public DateTime Moment { get; set; }

        public IEnumerable<IngredientEatingForCreateDto> IngredientsWithGrams { get; set; }

        public EatingForCreateDto()
        {
            IngredientsWithGrams = new List<IngredientEatingForCreateDto>();
        }
    }
}
