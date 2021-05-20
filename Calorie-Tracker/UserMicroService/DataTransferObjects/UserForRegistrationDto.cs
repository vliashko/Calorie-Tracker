using System.ComponentModel.DataAnnotations;

namespace UserMicroService.DataTransferObjects
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
