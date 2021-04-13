using System;

namespace Entities.Models
{
    public class EatingUser
    {
        public Guid Id { get; set; }
        public Guid EatingId { get; set; }
        public Eating Eating { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
