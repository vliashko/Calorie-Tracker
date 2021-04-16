using CaloriesTracker.Contracts;
using CaloriesTracker.Entities;
using CaloriesTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesTracker.Repositories
{
    public class IngredientRepository : RepositoryBase<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateIngredient(Ingredient ingredient) => Create(ingredient);

        public void DeleteIngredient(Ingredient ingredient) => Delete(ingredient);

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(ingr => ingr.Name)
                .ToListAsync();

        public async Task<Ingredient> GetIngredientAsync(Guid ingredientId, bool trackChanges) =>
            await FindByCondition(ingr => ingr.Id.Equals(ingredientId), trackChanges)
                .SingleOrDefaultAsync();
    }
}
