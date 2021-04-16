using System;
using System.Collections.Generic;

namespace CaloriesTracker.Entities.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public double Weight { get; set; }
        public int Height { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Calories { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Eating> Eatings { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }

        public UserProfile()
        {
            Eatings = new List<Eating>();
            Activities = new List<Activity>();
            Recipes = new List<Recipe>();
            //DateTime now = DateTime.Today;
            //int age = now.Year - DateOfBirth.Year;
            //if (DateOfBirth > now.AddYears(-age))
            //    age--;
            //// Формулы по Харрису-Бенедикту
            //Calories = Gender == Gender.Male ? 66.5 + 13.75 * Weight + 5.003 * Height - 6.775 * age :
            //    655.1 + 9.563 * Weight + 1.85 * Height - 4.676 * age;
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
