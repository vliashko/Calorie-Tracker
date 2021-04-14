using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEatingRepository
    {
        Task<IEnumerable<Eating>> GetAllEatingsAsync(bool trackChanges);
        Task<IEnumerable<Eating>> GetAllEatingsForUserAsync(Guid userId, bool trackChanges);
        Task<Eating> GetEatingAsync(Guid eatingId, bool trackChanges);
        Task<Eating> GetEatingForUserAsync(Guid userId, Guid eatingId, bool trackChanges);
        void CreateEating(Eating eating);
        void DeleteEating(Eating eating);
    }
}
