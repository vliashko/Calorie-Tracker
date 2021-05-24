using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Marvin.JsonPatch;
using ActivityMicroService.DataTransferObjects;
using ActivityMicroService.Contracts;
using ActivityMicroService.Filter;

namespace ActivityMicroService.Controllers
{
    [Route("api/users/{userId}/activities")]
    [ApiController]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _service;

        public ActivitiesController(IActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities(Guid userId)
        {
            var activities = await _service.GetActivitiesForUserProfileForDateAsync(userId);
            return Ok(activities);
        }

        [HttpGet("{activityId}", Name = "GetActivity")]
        public async Task<IActionResult> GetActivity(Guid activityId)
        {
            var activity = await _service.GetActivityAsync(activityId);
            if (activity == null)
                return NotFound();
            return Ok(activity);
        }
        [HttpGet("days/{countDays}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDataForChart(Guid userId, int countDays)
        {
            var result = await _service.GetDataForChartAsync(userId, countDays);
            return Ok(result);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateActivity(Guid userId, [FromBody] ActivityForCreateDto activityDto)
        {
            var activityView = await _service.CreateActivityForUserProfileAsync(userId, activityDto);
            if (activityView == null)
                return NotFound();
            return CreatedAtRoute("GetActivity", new { userId, activityId = activityView.Id }, activityView);
        }
        [HttpDelete("{activityId}")]
        public async Task<IActionResult> DeleteActivity(Guid activityId)
        {
            var result = await _service.DeleteActivityAsync(activityId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{activityId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateActivity(Guid activityId, [FromBody] ActivityForUpdateDto activityDto)
        {
            var result = await _service.UpdateActivityAsync(activityId, activityDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{activityId}")]
        public async Task<IActionResult> PartiallyUpdateActivity(Guid activityId, [FromBody] JsonPatchDocument<ActivityForUpdateDto> patchDoc)
        {
            var result = await _service.PartiallyUpdateActivityAsync(activityId, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
