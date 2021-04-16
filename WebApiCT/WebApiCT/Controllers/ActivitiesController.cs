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
        private readonly IServiceManager serviceManager;

        public ActivitiesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetActivities(Guid userId)
        {
            var activities = await serviceManager.Activity.GetActivities(userId);
            if (activities == null)
                return NotFound();
            return Ok(activities);
        }
        [HttpGet("{activityId}", Name = "GetActivity")]
        public async Task<IActionResult> GetActivity(Guid userId, Guid activityId)
        {
            var activity = await serviceManager.Activity.GetActivity(userId, activityId);
            if (activity == null)
                return NotFound();
            return Ok(activity);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateActivity(Guid userId, [FromBody] ActivityForCreateDto activityDto)
        {
            var activityView = await serviceManager.Activity.CreateActivity(userId, activityDto);
            if (activityView == null)
                return NotFound();
            return CreatedAtRoute("GetActivity", new { userId, activityId = activityView.Id }, activityView);
        }
        [HttpDelete("{activityId}")]
        public async Task<IActionResult> DeleteActivity(Guid userId, Guid activityId)
        {
            var result = await serviceManager.Activity.DeleteActivity(userId, activityId);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPut("{activityId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateActivity(Guid userId, Guid activityId, [FromBody] ActivityForUpdateDto activityDto)
        {
            var result = await serviceManager.Activity.UpdateActivity(userId, activityId, activityDto);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch("{activityId}")]
        public async Task<IActionResult> PartiallyUpdateActivity(Guid userId, Guid activityId, [FromBody] JsonPatchDocument<ActivityForUpdateDto> patchDoc)
        {
            var result = await serviceManager.Activity.PartiallyUpdateActivity(userId, activityId, patchDoc);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
