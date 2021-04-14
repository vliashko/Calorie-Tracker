using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCT.ActionFilter;

namespace WebApiCT.Controllers
{
    [Route("api/users/{userId}/eatings")]
    [ApiController]
    public class EatingsController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public EatingsController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.logger = logger;
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEatings(Guid userId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var eatings = await repositoryManager.Eating.GetAllEatingsForUserAsync(userId, trackChanges: false);
            var eatingsDto = mapper.Map<IEnumerable<EatingForReadDto>>(eatings);
            return Ok(eatingsDto);
        }
        [HttpGet("{eatingId}", Name = "GetEating")]
        public async Task<IActionResult> GetEating(Guid userId, Guid eatingId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var eating = await repositoryManager.Eating.GetEatingForUserAsync(userId, eatingId, trackChanges: false);
            if (eating == null)
            {
                logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return NotFound();
            }
            var eatingDto = mapper.Map<EatingForReadDto>(eating);
            return Ok(eatingDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEating(Guid userId, [FromBody] EatingForCreateDto eatingDto)
        {
            var eatingEntity = mapper.Map<Eating>(eatingDto);
            repositoryManager.Eating.CreateEating(eatingEntity);
            var eatingView = mapper.Map<EatingForReadDto>(eatingEntity);

            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: true);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            EatingUserProfile eatingUser = new EatingUserProfile
            {
                EatingId = eatingView.Id,
                UserProfileId = userId
            };
            user.EatingUserProfile.Add(eatingUser);
            await repositoryManager.SaveAsync();
            return CreatedAtRoute("GetEating", new { userId, eatingId = eatingView.Id }, eatingView);
        }
        [HttpDelete("{eatingId}")]
        public async Task<IActionResult> DeleteEating(Guid userId, Guid eatingId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var eating = await repositoryManager.Eating.GetEatingForUserAsync(userId, eatingId, trackChanges: false);
            if (eating == null)
            {
                logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return NotFound();
            }
            repositoryManager.Eating.DeleteEating(eating);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPut("{eatingId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEating(Guid userId, Guid eatingId, [FromBody] EatingForUpdateDto eatingDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var eating = await repositoryManager.Eating.GetEatingForUserAsync(userId, eatingId, trackChanges: true);
            if (eating == null)
            {
                logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return NotFound();
            }
            mapper.Map(eatingDto, eating);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{eatingId}")]
        public async Task<IActionResult> PartiallyUpdateEating(Guid userId, Guid eatingId, [FromBody] JsonPatchDocument<EatingForUpdateDto> patchDoc)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return NotFound();
            }
            var eating = await repositoryManager.Eating.GetEatingForUserAsync(userId, eatingId, trackChanges: true);
            if (eating == null)
            {
                logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return NotFound();
            }
            var eatingToPatch = mapper.Map<EatingForUpdateDto>(eating);
            patchDoc.ApplyTo(eatingToPatch, ModelState);
            TryValidateModel(eatingToPatch);
            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            mapper.Map(eatingToPatch, eating);
            await repositoryManager.SaveAsync();
            return NoContent();
        }
    }
}
