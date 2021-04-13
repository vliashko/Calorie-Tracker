using System;

namespace Entities.Models
{
    public class ActivityUser
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
