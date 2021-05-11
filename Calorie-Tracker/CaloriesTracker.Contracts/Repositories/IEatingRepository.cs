using CaloriesTracker.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Contracts
{
    public interface IEatingRepository
    {
        Task<IEnumerable<Eating>> GetAllEatingsForUserForDateAsync(Guid userId, DateTime dateTime, bool trackChanges);
        Task<IEnumerable<Eating>> GetAllEatingsForUserPerDays(Guid userId, int days, bool trackChanges);
        Task<Eating> GetEatingAsync(Guid eatingId, bool trackChanges);
        void CreateEating(Eating eating);
        void DeleteEating(Eating eating);
    }
}
