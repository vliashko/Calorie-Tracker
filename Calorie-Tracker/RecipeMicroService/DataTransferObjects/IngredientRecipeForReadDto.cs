namespace RecipeMicroService.DataTransferObjects
{
    public class IngredientRecipeForReadDto
    {
        public string Name { get; set; }
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public float Grams { get; set; }
    }
}
