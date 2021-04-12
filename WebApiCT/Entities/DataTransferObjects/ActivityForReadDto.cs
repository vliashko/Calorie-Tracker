using System;

namespace Entities.DataTransferObjects
{
    public class ActivityForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
