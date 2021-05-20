using Marvin.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserMicroService.Contracts;
using UserMicroService.DataTransferObjects;
using UserMicroService.Filter;

namespace UserMicroService.Controllers
{
    [Route("api/userprofiles")]
    [ApiController]
    [Authorize]
    public class UserProfilesController : ControllerBase
    {
        private readonly IUserProfileService _service;

        public UserProfilesController(IUserProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfileByUserId(string userId)
        {
            var user = await _service.GetUserProfileByUserIdAsync(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileForCreateDto userDto)
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == "userId").Value;
            var userView = await _service.CreateUserProfileForUserAsync(id, userDto);
            if (userView == null)
                return NotFound();
            return CreatedAtRoute("UserById", new { id = userView.Id }, userView);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserProfileForUpdateDto userDto)
        {
            var result = await _service.UpdateUserProfileAsync(id, userDto);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateUser(Guid id, [FromBody] JsonPatchDocument<UserProfileForUpdateDto> patchDoc)
        {
            var result = await _service.PartiallyUpdateUserProfileAsync(id, patchDoc);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
