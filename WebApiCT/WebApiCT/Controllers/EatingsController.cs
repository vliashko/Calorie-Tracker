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
    //[Authorize]
    public class EatingsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public EatingsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetEatings(Guid userId)
        {
            var eatings = await serviceManager.Eating.GetEatings(userId);
            if (eatings == null)
                return NotFound();
            return Ok(eatings);
        }
        [HttpGet("{eatingId}", Name = "GetEating")]
        public async Task<IActionResult> GetEating(Guid userId, Guid eatingId)
        {
            var eating = await serviceManager.Eating.GetEating(userId, eatingId);
            if (eating == null)
                return NotFound();
            return Ok(eating);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEating(Guid userId, [FromBody] EatingForCreateDto eatingDto)
        {
            var eatingView = await serviceManager.Eating.CreateEating(userId, eatingDto);
            if (eatingView == null)
                return NotFound();
            return CreatedAtRoute("GetEating", new { userId, eatingId = eatingView.Id }, eatingView);
        }
        [HttpDelete("{eatingId}")]
        public async Task<IActionResult> DeleteEating(Guid userId, Guid eatingId)
        {
            var result = await serviceManager.Eating.DeleteEating(userId, eatingId);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPut("{eatingId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEating(Guid userId, Guid eatingId, [FromBody] EatingForUpdateDto eatingDto)
        {
            var result = await serviceManager.Eating.UpdateEating(userId, eatingId, eatingDto);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch("{eatingId}")]
        public async Task<IActionResult> PartiallyUpdateEating(Guid userId, Guid eatingId, [FromBody] JsonPatchDocument<EatingForUpdateDto> patchDoc)
        {
            var result = await serviceManager.Eating.PartiallyUpdateEating(userId, eatingId, patchDoc);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
