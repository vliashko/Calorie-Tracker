using System;

namespace Entities.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public double Grams { get; set; }
        public Guid EatingId { get; set; }
    }
}
