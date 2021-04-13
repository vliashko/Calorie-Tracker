using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ActivityForUpdateDto
    {
        [Required(ErrorMessage = "Name is required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Start is required field.")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Finish is required field.")]
        public DateTime Finish { get; set; }
    }
}
