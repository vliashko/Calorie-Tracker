using System;
using System.Collections.Generic;

namespace CaloriesTracker.Entities.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float Calories
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - DateOfBirth.Year;
                if (DateOfBirth > now.AddYears(-age))
                    age--;
                // Формулы по Харрису-Бенедикту
                return Gender == Gender.Male ? 66.5f + 13.75f * Weight + 5.003f * Height - 6.775f * age :
                    655.1f + 9.563f * Weight + 1.85f * Height - 4.676f * age;
            }
            set { }
        }
        public float CurrentCalories
        {
            get
            {
                if (Eatings.Count == 0 && Activities.Count == 0)
                    return 0.0f;
                float calor = 0.0f;
                foreach (var eating in Eatings)
                {
                    calor += eating.TotalCalories;
                }
                foreach (var activity in Activities)
                {
                    calor -= activity.TotalCaloriesSpent;
                }
                return calor;
            }
            set { }
        }
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
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
