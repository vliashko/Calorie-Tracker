using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Marvin.JsonPatch;
using EatingMicroService.DataTransferObjects;
using EatingMicroService.Contracts;
using EatingMicroService.Filter;

namespace EatingMicroService.Controllers
{
    [Route("api/users/{userId}/eatings")]
    [ApiController]
    [Authorize]
    public class EatingsController : ControllerBase
    {
        private readonly IEatingService _service;

        public EatingsController(IEatingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEatings(Guid userId)
        {
            var eatings = await _service.GetEatingsForUserProfileForDateAsync(userId);
            return Ok(eatings);
        }
        [HttpGet("{eatingId}", Name = "GetEating")]
        public async Task<IActionResult> GetEating(Guid eatingId)
        {
            var eating = await _service.GetEatingAsync(eatingId);
            if (eating == null)
                return NotFound();
            return Ok(eating);
        }
        [HttpGet("days/{countDays}")]
        public async Task<IActionResult> GetDataForChart(Guid userId, int countDays)
        {
            var result = await _service.GetDataForChartAsync(userId, countDays);
            return Ok(result);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEating(Guid userId, [FromBody] EatingForCreateDto eatingDto)
        {
            var eatingView = await _service.CreateEatingForUserProfileAsync(userId, eatingDto);
            if (eatingView == null)
                return NotFound();
            return CreatedAtRoute("GetEating", new { userId, eatingId = eatingView.Id }, eatingView);
        }
        [HttpDelete("{eatingId}")]
        public async Task<IActionResult> DeleteEating(Guid eatingId)
        {
            var result = await _service.DeleteEatingAsync(eatingId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("{eatingId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEating(Guid eatingId, [FromBody] EatingForUpdateDto eatingDto)
        {
            var result = await _service.UpdateEatingAsync(eatingId, eatingDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{eatingId}")]
        public async Task<IActionResult> PartiallyUpdateEating(Guid eatingId, [FromBody] JsonPatchDocument<EatingForUpdateDto> patchDoc)
        {
            var result = await _service.PartiallyUpdateEatingAsync(eatingId, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
