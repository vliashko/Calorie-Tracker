using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CaloriesTracker.Api.Filter;
using Marvin.JsonPatch;
using CaloriesTracker.Services.Interfaces;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/users/{userId}/activities")]
    [ApiController]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ActivitiesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities(Guid userId)
        {
            var activities = await _serviceManager.Activity.GetActivitiesForUserProfileForDateAsync(userId);
            return Ok(activities);
        }

        [HttpGet("{activityId}", Name = "GetActivity")]
        public async Task<IActionResult> GetActivity(Guid activityId)
        {
            var activity = await _serviceManager.Activity.GetActivityAsync(activityId);
            if (activity == null)
                return NotFound();
            return Ok(activity);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateActivity(Guid userId, [FromBody] ActivityForCreateDto activityDto)
        {
            var activityView = await _serviceManager.Activity.CreateActivityForUserProfileAsync(userId, activityDto);
            if (activityView == null)
                return NotFound();
            return CreatedAtRoute("GetActivity", new { userId, activityId = activityView.Id }, activityView);
        }
        [HttpDelete("{activityId}")]
        public async Task<IActionResult> DeleteActivity(Guid activityId)
        {
            var result = await _serviceManager.Activity.DeleteActivityAsync(activityId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{activityId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateActivity(Guid activityId, [FromBody] ActivityForUpdateDto activityDto)
        {
            var result = await _serviceManager.Activity.UpdateActivityAsync(activityId, activityDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{activityId}")]
        public async Task<IActionResult> PartiallyUpdateActivity(Guid activityId, [FromBody] JsonPatchDocument<ActivityForUpdateDto> patchDoc)
        {
            var result = await _serviceManager.Activity.PartiallyUpdateActivityAsync(activityId, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
