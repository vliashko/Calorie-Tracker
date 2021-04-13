using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class EatingForUpdateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Moment is required field.")]
        public DateTime Moment { get; set; }
    }
}
