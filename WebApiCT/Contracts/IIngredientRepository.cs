using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllIngredientsAsync(bool trackChanges);
        Task<Ingredient> GetIngredientAsync(Guid ingredientId, bool trackChanges);
        void CreateIngredient(Ingredient ingredient);
        void DeleteIngredient(Ingredient ingredient);
    }
}
