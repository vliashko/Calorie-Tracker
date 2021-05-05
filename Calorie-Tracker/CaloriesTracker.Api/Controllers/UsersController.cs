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
using System.Collections.Generic;

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
            var userProfiles = await serviceManager.User.GetUsers();
            var usersResult = new List<UserForReadDto>();
            foreach (var userProfile in userProfiles)
            {
                var user = await userManager.FindByIdAsync(userProfile.UserId);
                var tmp = new UserForReadDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    UserProfile = userProfile
                };
                usersResult.Add(tmp);
            }
            return Ok(usersResult);
        }
        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userProfile = await serviceManager.UserProfile.GetUserProfileByUserId(id);
            if (userProfile == null)
                return NotFound();
            var result = new UserForReadDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                UserProfile = userProfile
            };
            return Ok(result);
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
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userProfile = await serviceManager.UserProfile.GetUserProfileByUserId(id);
            var delProfile = await serviceManager.User.DeleteUser(userProfile.Id);
            if (!delProfile)
                return NotFound();
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
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
