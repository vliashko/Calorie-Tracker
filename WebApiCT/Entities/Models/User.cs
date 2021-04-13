using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public double Weight { get; set; }
        public int Height { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<EatingUser> EatingUser { get; set; } = new List<EatingUser>();
        public virtual ICollection<ActivityUser> ActivityUser { get; set; } = new List<ActivityUser>();
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public User()
        {
            //DateTime now = DateTime.Today;
            //int age = now.Year - DateOfBirth.Year;
            //if (DateOfBirth > now.AddYears(-age))
            //    age--;
            //// Формулы по Харрису-Бенедикту
            //Calories = Sex == Sex.Male ? 66.5 + 13.75 * Weight + 5.003 * Height - 6.775 * age : 
            //    655.1 + 9.563 * Weight + 1.85 * Height - 4.676 * age;
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
