using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UserMicroService.Contracts;
using UserMicroService.DataTransferObjects;
using UserMicroService.Filter;
using UserMicroService.Models.Pagination;

namespace UserMicroService.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("page/{number}/size/{pageSize}/params")]
        public async Task<IActionResult> GetUsers(string userName = "", string email = "", int pageSize = 5, int number = 1)
        {
            var userSearch = new UserSearchModelDto { UserName = userName, Email = email };
            var users = await _service.GetUsersPaginationAsync(pageSize, number, userSearch);
            var count = await _service.GetUsersCount(userSearch);
            PageViewModel page = new PageViewModel(count, number, pageSize);
            ViewModel<UserForReadDto> userViewModel = new ViewModel<UserForReadDto> { PageViewModel = page, Objects = users };
            return Ok(userViewModel);
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _service.GetUserAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("confirmedemail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersWithConfirmedEmail()
        {
            var users = await _service.GetUsersWithConfirmedEmail();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _service.DeleteUserAsync(id);
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserForUpdateDto userDto)
        {
            var result = await _service.UpdateUserAsync(id, userDto);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
