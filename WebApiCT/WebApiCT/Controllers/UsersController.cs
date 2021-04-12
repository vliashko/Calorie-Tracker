using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiCT.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public UsersController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.logger = logger;
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repositoryManager.User.GetAllUsersAsync(trackChanges: false);
            var usersDto = mapper.Map<IEnumerable<User>>(users);
            return Ok(usersDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await repositoryManager.User.GetUserAsync(id, trackChanges: false);
            if(user == null)
            {
                logger.LogInfo($"User with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var userDto = mapper.Map<User>(user);
            return Ok(userDto);
        }
    }
}
