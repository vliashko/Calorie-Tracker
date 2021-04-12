using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
