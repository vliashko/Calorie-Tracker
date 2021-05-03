using CaloriesTracker.Entities.Models;
using System;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class UserProfileForReadDto
    {
        public Guid Id { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float Calories { get; set; }
        public float CurrentCalories { get; set; }
        public string UserId { get; set; }
    }
}
