using CaloriesTracker.Api.Filter;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/userprofiles")]
    [ApiController]
    [Authorize]
    public class UserProfilesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserProfilesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfileByUserId(string userId)
        {
            var user = await _serviceManager.UserProfile.GetUserProfileByUserIdAsync(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpGet("{countDays}")]
        public async Task<IActionResult> GetDataForChart(Guid userId, int countDays)
        {
            var result = await _serviceManager.UserProfile.GetDataForChartAsync(userId, countDays);
            return Ok(result);
        }
        [HttpPost("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateUserProfile(string id, [FromBody] UserProfileForCreateDto userDto)
        {
            var userView = await _serviceManager.UserProfile.CreateUserProfileForUserAsync(id, userDto);
            if (userView == null)
                return NotFound();
            return CreatedAtRoute("UserById", new { id = userView.Id }, userView);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserProfileForUpdateDto userDto)
        {
            var result = await _serviceManager.UserProfile.UpdateUserProfileAsync(id, userDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateUser(Guid id, [FromBody] JsonPatchDocument<UserProfileForUpdateDto> patchDoc)
        {
            var result = await _serviceManager.UserProfile.PartiallyUpdateUserProfileAsync(id, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
