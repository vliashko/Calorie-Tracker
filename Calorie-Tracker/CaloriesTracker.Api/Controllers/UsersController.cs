using CaloriesTracker.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CaloriesTracker.Services.Interfaces;
using CaloriesTracker.Api.Filter;
using CaloriesTracker.Entities.Pagination;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UsersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("page/{number}/size/{pageSize}/params")]
        public async Task<IActionResult> GetUsers(string userName = "", string email = "", int pageSize = 5, int number = 1)
        {
            var userSearch = new UserSearchModelDto { UserName = userName, Email = email };
            var users = await _serviceManager.User.GetUsersPaginationAsync(pageSize, number, userSearch);
            var count = await _serviceManager.User.GetUsersCount(userSearch);
            PageViewModel page = new PageViewModel(count, number, pageSize);
            ViewModel<UserForReadDto> userViewModel = new ViewModel<UserForReadDto> { PageViewModel = page, Objects = users };
            return Ok(userViewModel);
        }
        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _serviceManager.User.GetUserAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _serviceManager.User.DeleteUserAsync(id);
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserForUpdateDto userDto)
        {
            var result = await _serviceManager.User.UpdateUserAsync(id, userDto);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
