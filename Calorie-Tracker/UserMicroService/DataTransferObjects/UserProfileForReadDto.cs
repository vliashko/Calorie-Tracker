using System;
using UserMicroService.Models;

namespace UserMicroService.DataTransferObjects
{
    public class UserProfileForReadDto
    {
        public Guid Id { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float Calories { get; set; }
    }
}
