using AutoMapper;
using EatingMicroService.Contracts;
using EatingMicroService.DataTransferObjects;
using EatingMicroService.Models;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatingMicroService.Services
{
    public class EatingService : IEatingService
    {
        private readonly IEatingRepository _repository;
        private readonly IMapper _mapper;

        public EatingService(IEatingRepository repository,
                             IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EatingForReadDto> CreateEatingForUserProfileAsync(Guid id, EatingForCreateDto eatingDto)
        {
            var eatingEntity = _mapper.Map<Eating>(eatingDto);
            eatingEntity.UserProfileId = id;
            _repository.CreateEating(eatingEntity);
            var eatingView = _mapper.Map<EatingForReadDto>(eatingEntity);
            await _repository.SaveAsync();
            return eatingView;
        }

        public async Task<MessageDetailsDto> DeleteEatingAsync(Guid eatingId)
        {
            var eating = await _repository.GetEatingAsync(eatingId, trackChanges: false);
            if (eating == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Eating with id: {eatingId} doesn't exist in the database" };
            }
            _repository.DeleteEating(eating);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<EatingForReadDto> GetEatingAsync(Guid eatingId)
        {
            var eating = await _repository.GetEatingAsync(eatingId, trackChanges: false);
            if (eating == null)
            {
                return null;
            }
            var eatingDto = _mapper.Map<EatingForReadDto>(eating);
            return eatingDto;
        }

        public async Task<IEnumerable<EatingForReadDto>> GetEatingsForUserProfileForDateAsync(Guid id)
        {
            DateTime dateTime = DateTime.Now;
            var eatings = await _repository.GetAllEatingsForUserForDateAsync(id, dateTime, trackChanges: false);
            var eatingsDto = _mapper.Map<IEnumerable<EatingForReadDto>>(eatings);
            return eatingsDto;
        }

        public async Task<IEnumerable<EatingForReadDto>> GetEatingsForUserProfilePerDaysAsync(Guid id, int days)
        {
            var eatings = await _repository.GetAllEatingsForUserPerDays(id, days, trackChanges: false);
            var eatingsDto = _mapper.Map<IEnumerable<EatingForReadDto>>(eatings);
            return eatingsDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateEatingAsync(Guid eatingId, JsonPatchDocument<EatingForUpdateDto> patchDoc)
        {
            var eating = await _repository.GetEatingAsync(eatingId, trackChanges: true);
            if (eating == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Eating with id: {eatingId} doesn't exist in the database" };
            }
            var eatingToPatch = _mapper.Map<EatingForUpdateDto>(eating);
            patchDoc.ApplyTo(eatingToPatch);
            _mapper.Map(eatingToPatch, eating);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateEatingAsync(Guid eatingId, EatingForUpdateDto eatingDto)
        {
            var eating = await _repository.GetEatingAsync(eatingId, trackChanges: true);
            if (eating == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Eating with id: {eatingId} doesn't exist in the database" };
            }
            _mapper.Map(eatingDto, eating);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<IEnumerable<DayForChartDto>> GetDataForChartAsync(Guid id, int countDays)
        {
            var eatings = await _repository.GetAllEatingsForUserPerDays(id, countDays, trackChanges: false);
            var resultEatings = eatings
                .GroupBy(x => x.Moment.Date)
                .Select(x =>
                    new DayForChartDto { Day = x.Key, CurrentCalories = x.Sum(k => k.TotalCalories) });

            var result = new List<DayForChartDto>();
            for (int i = 0; i < countDays; i++)
            {
                var curDate = DateTime.Now.Date - new TimeSpan(24 * i, 0, 0);
                var resultAdd = new DayForChartDto { Day = curDate, CurrentCalories = 0 };
                var tempEat = resultEatings.SingleOrDefault(x => x.Day == curDate);
                if (tempEat != null)
                    resultAdd.CurrentCalories += tempEat.CurrentCalories;
                result.Add(resultAdd);
            }

            return result.OrderBy(x => x.Day);
        }
    }
}
