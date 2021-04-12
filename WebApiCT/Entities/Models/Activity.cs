using System;
using System.Collections.Generic;

namespace Entities.Models
{
    /// <summary>
    /// Физическая активность (Набор упражнений)
    /// </summary>
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
