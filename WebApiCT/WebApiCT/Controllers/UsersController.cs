using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCT.ActionFilter;

namespace WebApiCT.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UsersController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager)
        {
            this.logger = logger;
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repositoryManager.User.GetAllUsersAsync(trackChanges: false);
            var usersDto = mapper.Map<IEnumerable<UserProfileForReadDto>>(users);
            return Ok(usersDto);
        }
        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await repositoryManager.User.GetUserAsync(id, trackChanges: false);
            if(user == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var userDto = mapper.Map<UserProfileForReadDto>(user);
            return Ok(userDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateUser([FromBody] UserProfileForCreateDto userDto)
        {
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            var userEntity = mapper.Map<UserProfile>(userDto);
            userEntity.UserId = user.Id;
            repositoryManager.User.CreateUser(userEntity);
            await repositoryManager.SaveAsync();
            var userView = mapper.Map<UserProfileForReadDto>(userEntity);
            return CreatedAtRoute("UserById", new { id = userView.Id }, userView);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await repositoryManager.User.GetUserAsync(id, trackChanges: false);
            if(user == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return NotFound();
            }
            repositoryManager.User.DeleteUser(user);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserProfileForUpdateDto userDto)
        {
            var userEntity = await repositoryManager.User.GetUserAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return NotFound();
            }
            mapper.Map(userDto, userEntity);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateUser(Guid id, [FromBody] JsonPatchDocument<UserProfileForUpdateDto> patchDoc)
        {
            var userEntity = await repositoryManager.User.GetUserAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var userToPatch = mapper.Map<UserProfileForUpdateDto>(userEntity);

            patchDoc.ApplyTo(userToPatch, ModelState);
            TryValidateModel(userToPatch);
            if(!ModelState.IsValid)
            {
                logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            mapper.Map(userToPatch, userEntity);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
    }
}
