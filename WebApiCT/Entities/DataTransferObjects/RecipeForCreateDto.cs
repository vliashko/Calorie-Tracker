using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class RecipeForCreateDto
    { 
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Instruction is required field.")]
        public string Instruction { get; set; }

        public IEnumerable<IngredientRecipeForCreateDto> IngredientsWithGrams { get; set; }

        public RecipeForCreateDto()
        {
            IngredientsWithGrams = new List<IngredientRecipeForCreateDto>();
        }
    }
}
