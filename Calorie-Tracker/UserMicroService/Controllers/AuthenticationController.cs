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

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }
        [HttpPost]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userDto)
        {
            var result = await _service.RegisterUser(userDto);
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
    }
}
