using System;
using System.Collections.Generic;

namespace Entities.Models
{
    /// <summary>
    /// Прием пищи
    /// </summary>
    public class Eating
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Moment { get; set; }
        public ICollection<Product> Products { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
