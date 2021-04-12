using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<IngredientRecipe> IngredientRecipe { get; set; } = new List<IngredientRecipe>();
    }
}
