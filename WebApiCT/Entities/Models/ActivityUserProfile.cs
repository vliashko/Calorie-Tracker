using System;

namespace Entities.Models
{
    public class ActivityUserProfile
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
