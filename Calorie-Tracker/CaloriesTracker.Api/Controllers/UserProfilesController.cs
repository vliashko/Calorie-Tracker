using CaloriesTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/userprofiles")]
    [ApiController]
    [Authorize]
    public class UserProfilesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public UserProfilesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfileByUserId(string userId)
        {
            var user = await serviceManager.UserProfile.GetUserProfileByUserId(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
