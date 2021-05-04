using System;

namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class DayForChartDto
    {
        public DateTime Day { get; set; }
        public float CurrentCalories { get; set; }
    }
}
