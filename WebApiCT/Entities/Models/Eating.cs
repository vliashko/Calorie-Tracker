using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Eating
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Moment { get; set; }
        public virtual ICollection<IngredientEating> IngredientEating { get; set; } = new List<IngredientEating>();
        public virtual ICollection<EatingUserProfile> EatingUserProfile { get; set; } = new List<EatingUserProfile>();
    }
}
