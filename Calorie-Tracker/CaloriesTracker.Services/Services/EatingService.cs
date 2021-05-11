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
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EatingService(IRepositoryManager repositoryManager, 
                             ILoggerManager logger, 
                             IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<EatingForReadDto> CreateEatingForUserProfileAsync(Guid id, EatingForCreateDto eatingDto)
        {
            var user = await _repositoryManager.User.GetUserProfileAsync(id, trackChanges: true);
            if (user == null)
            {
                _logger.LogInfo($"UserProfile with id: {id} doesn't exist in the database");
                return null;
            }
            
            foreach (var iteration in eatingDto.IngredientsWithGrams)
            {
                iteration.Ingredient = await _repositoryManager.Ingredient.GetIngredientAsync(iteration.IngredientId, true);
            }

            var eatingEntity = _mapper.Map<Eating>(eatingDto);
            _repositoryManager.Eating.CreateEating(eatingEntity);
            var eatingView = _mapper.Map<EatingForReadDto>(eatingEntity);
            user.Eatings.Add(eatingEntity);
            await _repositoryManager.SaveAsync();
            return eatingView;
        }

        public async Task<MessageDetailsDto> DeleteEatingAsync(Guid eatingId)
        {
            var eating = await _repositoryManager.Eating.GetEatingAsync(eatingId, trackChanges: false);
            if (eating == null)
            {
                _logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Eating with id: {eatingId} doesn't exist in the database" };
            }
            _repositoryManager.Eating.DeleteEating(eating);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<EatingForReadDto> GetEatingAsync(Guid eatingId)
        {
            var eating = await _repositoryManager.Eating.GetEatingAsync(eatingId, trackChanges: false);
            if (eating == null)
            {
                _logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return null;
            }
            var eatingDto = _mapper.Map<EatingForReadDto>(eating);
            return eatingDto;
        }

        public async Task<IEnumerable<EatingForReadDto>> GetEatingsForUserProfileForDateAsync(Guid id)
        {
            DateTime dateTime = DateTime.Now;
            var eatings = await _repositoryManager.Eating.GetAllEatingsForUserForDateAsync(id, dateTime, trackChanges: false);
            var eatingsDto = _mapper.Map<IEnumerable<EatingForReadDto>>(eatings);
            return eatingsDto;
        }

        public async Task<IEnumerable<EatingForReadDto>> GetEatingsForUserProfilePerDaysAsync(Guid id, int days)
        {
            var eatings = await _repositoryManager.Eating.GetAllEatingsForUserPerDays(id, days, trackChanges: false);
            var eatingsDto = _mapper.Map<IEnumerable<EatingForReadDto>>(eatings);
            return eatingsDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateEatingAsync(Guid eatingId, JsonPatchDocument<EatingForUpdateDto> patchDoc)
        {
            var eating = await _repositoryManager.Eating.GetEatingAsync(eatingId, trackChanges: true);
            if (eating == null)
            {
                _logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Eating with id: {eatingId} doesn't exist in the database" };
            }
            var eatingToPatch = _mapper.Map<EatingForUpdateDto>(eating);
            patchDoc.ApplyTo(eatingToPatch);
            _mapper.Map(eatingToPatch, eating);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateEatingAsync(Guid eatingId, EatingForUpdateDto eatingDto)
        {
            var eating = await _repositoryManager.Eating.GetEatingAsync(eatingId, trackChanges: true);
            if (eating == null)
            {
                _logger.LogInfo($"Eating with id: {eatingId} doesn't exist in the database");
                return new MessageDetailsDto { StatusCode = 404, Message = $"Eating with id: {eatingId} doesn't exist in the database" };
            }
            foreach (var iteration in eatingDto.IngredientsWithGrams)
            {
                iteration.Ingredient = await _repositoryManager.Ingredient.GetIngredientAsync(iteration.IngredientId, true);
            }
            _mapper.Map(eatingDto, eating);
            await _repositoryManager.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
