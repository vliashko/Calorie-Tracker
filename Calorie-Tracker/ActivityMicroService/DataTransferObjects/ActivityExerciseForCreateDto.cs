using System;
using System.ComponentModel.DataAnnotations;

namespace ActivityMicroService.DataTransferObjects
{
    public class ActivityExerciseForCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float CaloriesSpent { get; set; }
        [Range(1, 200, ErrorMessage = "NumberOfRepetitions bust be betwewn 1 and 200")]
        public int NumberOfRepetitions { get; set; }
        [Range(1, 200, ErrorMessage = "NumberOfSets bust be betwewn 1 and 200")]
        public int NumberOfSets { get; set; }
    }
}
