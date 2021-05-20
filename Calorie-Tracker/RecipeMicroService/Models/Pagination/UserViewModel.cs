using System.Collections.Generic;

namespace RecipeMicroService.Models.Pagination
{
    public class ViewModel<T>
    {
        public IEnumerable<T> Objects { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
