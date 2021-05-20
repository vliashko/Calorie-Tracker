using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeMicroService.DataTransferObjects
{
    public class RecipeForUpdateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Instruction is required field.")]
        public string Instruction { get; set; }

        public IEnumerable<IngredientRecipeForUpdateDto> IngredientsWithGrams { get; set; }

        public RecipeForUpdateDto()
        {
            IngredientsWithGrams = new List<IngredientRecipeForUpdateDto>();
        }
    }
}
