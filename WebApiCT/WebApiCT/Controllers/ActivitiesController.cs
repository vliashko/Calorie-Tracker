using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaloriesTracker.Api.Filter;

namespace CaloriesTracker.Api.Controllers
{
    [Route("api/users/{userId}/activities")]
    [ApiController]
    [Authorize]
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
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
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
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: false);
            if (activity == null)
            {
                logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return NotFound();
            }
            var activityDto = mapper.Map<ActivityForReadDto>(activity);
            return Ok(activityDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateActivity(Guid userId, [FromBody] ActivityForCreateDto activityDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: true);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var activityEntity = mapper.Map<Activity>(activityDto);
            repositoryManager.Activity.CreateActivity(activityEntity);
            var activityView = mapper.Map<ActivityForReadDto>(activityEntity);
            user.Activities.Add(activityEntity);
            await repositoryManager.SaveAsync();
            return CreatedAtRoute("GetActivity", new { userId, activityId = activityView.Id }, activityView);
        }
        [HttpDelete("{activityId}")]
        public async Task<IActionResult> DeleteActivity(Guid userId, Guid activityId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: false);
            if(activity == null)
            {
                logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return NotFound();
            }
            repositoryManager.Activity.DeleteActivity(activity);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPut("{activityId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateActivity(Guid userId, Guid activityId, [FromBody] ActivityForUpdateDto activityDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: true);
            if (activity == null)
            {
                logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return NotFound();
            }
            mapper.Map(activityDto, activity);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{activityId}")]
        public async Task<IActionResult> PartiallyUpdateActivity(Guid userId, Guid activityId, [FromBody] JsonPatchDocument<ActivityForUpdateDto> patchDoc)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var activity = await repositoryManager.Activity.GetActivityForUserAsync(userId, activityId, trackChanges: true);
            if (activity == null)
            {
                logger.LogInfo($"Activity with id: {activityId} doesn't exist in the database");
                return NotFound();
            }
            var activityToPatch = mapper.Map<ActivityForUpdateDto>(activity);
            patchDoc.ApplyTo(activityToPatch, ModelState);
            TryValidateModel(activityToPatch);
            if(!ModelState.IsValid)
            {
                logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            mapper.Map(activityToPatch, activity);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
    }
}
