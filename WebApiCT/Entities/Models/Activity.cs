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
        public virtual ICollection<ActivityExercise> ActivityExercise { get; set; } = new List<ActivityExercise>();
        public virtual ICollection<ActivityUser> ActivityUser { get; set; } = new List<ActivityUser>();
    }
}
