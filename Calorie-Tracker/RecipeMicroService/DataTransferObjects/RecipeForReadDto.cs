using System;
using System.Collections.Generic;

namespace RecipeMicroService.DataTransferObjects
{
    public class RecipeForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public float TotalCalories { get; set; }

        public IEnumerable<IngredientRecipeForReadDto> IngredientsWithGrams { get; set; }

        public RecipeForReadDto()
        {
            IngredientsWithGrams = new List<IngredientRecipeForReadDto>();
        }
    }
}
