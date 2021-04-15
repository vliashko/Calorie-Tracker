using Entities.Models;
using System;

namespace Entities.DataTransferObjects
{
    public class UserProfileForReadDto
    {
        public Guid Id { get; set; }
        public double Weight { get; set; }
        public int Height { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
