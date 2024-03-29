﻿using System;
using System.ComponentModel.DataAnnotations;
using UserMicroService.Models;

namespace UserMicroService.DataTransferObjects
{
    public class UserProfileForUpdateDto
    {
        [Required(ErrorMessage = "Weight is required field.")]
        [Range(35, 350, ErrorMessage = "Weight must be between 35 and 350.")]
        public float Weight { get; set; }
        [Required(ErrorMessage = "Height is required field.")]
        [Range(120, 250, ErrorMessage = "Height must be between 120 and 250.")]
        public int Height { get; set; }
        [Required(ErrorMessage = "Gender is required field.")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Date Of Birth is required field.")]
        public DateTime DateOfBirth { get; set; }
    }
}
