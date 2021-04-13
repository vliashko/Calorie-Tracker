using System;

namespace Entities.DataTransferObjects
{
    public class EatingForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Moment { get; set; }
    }
}
