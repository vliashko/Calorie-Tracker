using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class RecipeForCreateDto
    { 
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required field.")]
        public string Description { get; set; }
    }
}
