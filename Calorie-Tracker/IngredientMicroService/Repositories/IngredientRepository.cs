using IngredientMicroService.Contracts;
using IngredientMicroService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngredientMicroService.Repositories
{
    public class IngredientRepository : RepositoryBase<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RepositoryDbContext context) : base(context)
        {
        }

        public async Task<int> CountOfIngredientsAsync(string searchName, bool trackChanges) => await FindByCondition(ingr => string.IsNullOrWhiteSpace(searchName) || ingr.Name.Contains(searchName), trackChanges).CountAsync();

        public void CreateIngredient(Ingredient ingredient) => Create(ingredient);

        public void DeleteIngredient(Ingredient ingredient) => Delete(ingredient);

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsPaginationAsync(int pageSize, int number, string searchName, bool trackChanges) =>
            await FindByCondition(ingr => string.IsNullOrWhiteSpace(searchName) || ingr.Name.Contains(searchName), trackChanges)
                .Skip((number - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(ingr => ingr.Name)
                .ToListAsync();

        public async Task<Ingredient> GetIngredientAsync(Guid ingredientId, bool trackChanges) =>
            await FindByCondition(ingr => ingr.Id.Equals(ingredientId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
