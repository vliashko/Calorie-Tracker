namespace ActivityMicroService.DataTransferObjects
{
    public class ActivityExerciseForReadDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float CaloriesSpent { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfSets { get; set; }
    }
}
