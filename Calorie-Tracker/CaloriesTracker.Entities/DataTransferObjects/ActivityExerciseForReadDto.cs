namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class ActivityExerciseForReadDto
    {
        public ExerciseForReadDto Exercise { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfSets { get; set; }
    }
}
