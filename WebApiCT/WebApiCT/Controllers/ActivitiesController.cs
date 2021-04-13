using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCT.ActionFilter;

namespace WebApiCT.Controllers
{
    [Route("api/users/{userId}/activities")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public ActivitiesController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.logger = logger;
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetActivities(Guid userId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"User with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var activities = await repositoryManager.Activity.GetAllActivitiesForUserAsync(userId, trackChanges: false);
            var activitiesDto = mapper.Map<IEnumerable<ActivityForReadDto>>(activities);
            return Ok(activitiesDto);
        }
        [HttpGet("{activityId}", Name = "GetActivity")]
        public async Task<IActionResult> GetActivity(Guid userId, Guid activityId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"User with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: false);
            var activityDto = mapper.Map<ActivityForReadDto>(activity);
            return Ok(activityDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateActivity(Guid userId, [FromBody] ActivityForCreateDto activityDto)
        {
            var activityEntity = mapper.Map<Activity>(activityDto);
            repositoryManager.Activity.CreateActivity(activityEntity);
            var activityView = mapper.Map<ActivityForReadDto>(activityEntity);

            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: true);
            if (user == null)
            {
                logger.LogInfo($"User with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            ActivityUser activityUser = new ActivityUser
            {
                ActivityId = activityView.Id,
                UserId = userId
            };
            user.ActivityUser.Add(activityUser);
            await repositoryManager.SaveAsync();
            return CreatedAtRoute("GetActivity", new { userId, activityId = activityView.Id }, activityView);
        }
    }
}
