using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserMicroService.Contracts;
using UserMicroService.DataTransferObjects;
using UserMicroService.Filter;

namespace UserMicroService.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        private readonly IEmailService _emailService;

        public AuthenticationController(IAuthenticationService service, IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
        }
        [HttpPost]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userDto)
        {
            var result = await _service.RegisterUser(userDto);
            if(result.StatusCode == 201)
            {
                var callBackUrl = Url.Action("ConfirmEmail", "Authentication", new { code = result.Message.Split("||")[0], id = result.Message.Split("||")[1] }, 
                    protocol: HttpContext.Request.Scheme, "localhost:5003");
                await _emailService.SendEmailAsync(userDto.Email, "Confirm your account", $"Confirm your account with link: <a href='{callBackUrl}'>link</a>");
            }
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPost("login")]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var result = await _service.Authenticate(user);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpGet("generatenewpassword/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GenerateNewPassword(string id)
        {
            var result = await _service.GenerateNewPassword(id);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string id, string code)
        {
            if (code == null || id == null)
            {
                return BadRequest();
            }
            var result = await _service.ConfirmEmail(id, code);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
