﻿using System;

namespace ExerciseMicroService.DataTransferObjects
{
    public class ExerciseForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float CaloriesSpent { get; set; }
    }
}
