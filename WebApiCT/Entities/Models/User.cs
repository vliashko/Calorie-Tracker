using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Логин является обязательным полем.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Вес является обязательным полем.")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Рост является обязательным полем.")]
        public int Height { get; set; }
        [Required(ErrorMessage = "Пол является обязательным полем.")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Дата рождения является обязательным полем.")]
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Eating> Eatings { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }

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
