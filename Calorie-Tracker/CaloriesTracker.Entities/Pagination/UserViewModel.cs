using System.Collections.Generic;

namespace CaloriesTracker.Entities.Pagination
{
    public class ViewModel<T>
    {
        public IEnumerable<T> Objects { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
