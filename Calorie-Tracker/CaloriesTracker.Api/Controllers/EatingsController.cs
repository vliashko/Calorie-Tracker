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
    [Route("api/users/{userId}/eatings")]
    [ApiController]
    [Authorize]
    public class EatingsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EatingsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetEatings(Guid userId)
        {
            var eatings = await _serviceManager.Eating.GetEatingsForUserProfileForDateAsync(userId);
            return Ok(eatings);
        }
        [HttpGet("{eatingId}", Name = "GetEating")]
        public async Task<IActionResult> GetEating(Guid eatingId)
        {
            var eating = await _serviceManager.Eating.GetEatingAsync(eatingId);
            if (eating == null)
                return NotFound();
            return Ok(eating);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEating(Guid userId, [FromBody] EatingForCreateDto eatingDto)
        {
            var eatingView = await _serviceManager.Eating.CreateEatingForUserProfileAsync(userId, eatingDto);
            if (eatingView == null)
                return NotFound();
            return CreatedAtRoute("GetEating", new { userId, eatingId = eatingView.Id }, eatingView);
        }
        [HttpDelete("{eatingId}")]
        public async Task<IActionResult> DeleteEating(Guid eatingId)
        {
            var result = await _serviceManager.Eating.DeleteEatingAsync(eatingId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{eatingId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEating(Guid eatingId, [FromBody] EatingForUpdateDto eatingDto)
        {
            var result = await _serviceManager.Eating.UpdateEatingAsync(eatingId, eatingDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{eatingId}")]
        public async Task<IActionResult> PartiallyUpdateEating(Guid eatingId, [FromBody] JsonPatchDocument<EatingForUpdateDto> patchDoc)
        {
            var result = await _serviceManager.Eating.PartiallyUpdateEatingAsync(eatingId, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
