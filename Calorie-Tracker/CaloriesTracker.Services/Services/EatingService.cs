using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.Services.Interfaces;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Services.Services
{
    public class EatingService : IEatingService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public EatingService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<EatingForReadDto> CreateEating(Guid userId, EatingForCreateDto eatingDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: true);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            foreach (var iteration in eatingDto.IngredientsWithGrams)
            {
                iteration.Ingredient = await GetIngredientById(iteration.IngredientId);
            }
            var eatingEntity = mapper.Map<Eating>(eatingDto);
            repositoryManager.Eating.CreateEating(eatingEntity);
            var eatingView = mapper.Map<EatingForReadDto>(eatingEntity);
            user.Eatings.Add(eatingEntity);
            await repositoryManager.SaveAsync();
            return eatingView;
        }

        public async Task<bool> DeleteEating(Guid userId, Guid eatingId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var eating = await repositoryManager.Eating.GetEatingForUserAsync(userId, eatingId, trackChanges: false);
            if (eating == null)
            {
                logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return false;
            }
            repositoryManager.Eating.DeleteEating(eating);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<EatingForReadDto> GetEating(Guid userId, Guid eatingId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            var eating = await repositoryManager.Eating.GetEatingForUserAsync(userId, eatingId, trackChanges: false);
            if (eating == null)
            {
                logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return null;
            }
            var eatingDto = mapper.Map<EatingForReadDto>(eating);
            return eatingDto;
        }

        public async Task<IEnumerable<EatingForReadDto>> GetEatings(Guid userId)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return null;
            }
            var eatings = await repositoryManager.Eating.GetAllEatingsForUserAsync(userId, trackChanges: false);
            var eatingsDto = mapper.Map<IEnumerable<EatingForReadDto>>(eatings);
            return eatingsDto;
        }

        public async Task<bool> PartiallyUpdateEating(Guid userId, Guid eatingId, JsonPatchDocument<EatingForUpdateDto> patchDoc)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var eating = await repositoryManager.Eating.GetEatingForUserAsync(userId, eatingId, trackChanges: true);
            if (eating == null)
            {
                logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return false;
            }
            var eatingToPatch = mapper.Map<EatingForUpdateDto>(eating);
            patchDoc.ApplyTo(eatingToPatch);
            mapper.Map(eatingToPatch, eating);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateEating(Guid userId, Guid eatingId, EatingForUpdateDto eatingDto)
        {
            var user = await repositoryManager.User.GetUserAsync(userId, trackChanges: false);
            if (user == null)
            {
                logger.LogInfo($"UserProfile with id: {userId} doesn't exist in the database");
                return false;
            }
            var eating = await repositoryManager.Eating.GetEatingForUserAsync(userId, eatingId, trackChanges: true);
            if (eating == null)
            {
                logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return false;
            }
            foreach (var iteration in eatingDto.IngredientsWithGrams)
            {
                iteration.Ingredient = await GetIngredientById(iteration.IngredientId);
            }
            mapper.Map(eatingDto, eating);
            await repositoryManager.SaveAsync();
            return true;
        }
        private async Task<Ingredient> GetIngredientById(Guid id)
        {
            return await repositoryManager.Ingredient.GetIngredientAsync(id, trackChanges: true);
        }
    }
}
