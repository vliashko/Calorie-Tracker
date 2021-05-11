using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CaloriesTracker.Api.Filter;
using CaloriesTracker.Services.Interfaces;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpPost]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userDto)
        {
            var result = await _serviceManager.Authentication.RegisterUser(userDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPost("login")]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var result = await _serviceManager.Authentication.Authenticate(user);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpGet("generatenewpassword/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GenerateNewPassword(string id)
        {
            var result = await _serviceManager.Authentication.GenerateNewPassword(id);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
