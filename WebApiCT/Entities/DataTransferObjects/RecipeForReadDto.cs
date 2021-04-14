using System;

namespace Entities.DataTransferObjects
{
    public class RecipeForReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
