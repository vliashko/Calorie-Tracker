using System;

namespace Entities.Models
{
    public class EatingUserProfile
    {
        public Guid Id { get; set; }
        public Guid EatingId { get; set; }
        public Eating Eating { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
