using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CaloriesTracker.Services.Interfaces;
using CaloriesTracker.Entities.Models;
using Microsoft.AspNetCore.Identity;
using CaloriesTracker.Api.Filter;
using Marvin.JsonPatch;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        private readonly UserManager<User> userManager;

        public UsersController(IServiceManager serviceManager, UserManager<User> userManager)
        {
            this.serviceManager = serviceManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await serviceManager.User.GetUsers());
        }
        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await serviceManager.User.GetUser(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateUser([FromBody] UserProfileForCreateDto userDto)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userView = await serviceManager.User.CreateUser(userDto, user.Id);
            return CreatedAtRoute("UserById", new { id = userView.Id }, userView);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await serviceManager.User.DeleteUser(id);
            if(!result)
                return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserProfileForUpdateDto userDto)
        {
            var result = await serviceManager.User.UpdateUser(id, userDto);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateUser(Guid id, [FromBody] JsonPatchDocument<UserProfileForUpdateDto> patchDoc)
        {
            var result = await serviceManager.User.PartiallyUpdateUser(id, patchDoc);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
