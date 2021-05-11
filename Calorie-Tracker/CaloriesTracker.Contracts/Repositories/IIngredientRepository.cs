using CaloriesTracker.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesTracker.Contracts
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllIngredientsPaginationAsync(int pageSize, int number, string searchName, bool trackChanges);
        Task<int> CountOfIngredientsAsync(string searchName, bool trackChanges);
        Task<Ingredient> GetIngredientAsync(Guid ingredientId, bool trackChanges);
        void CreateIngredient(Ingredient ingredient);
        void DeleteIngredient(Ingredient ingredient);
    }
}
