using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RepositoryDbContext context) : base(context)
        {
        }

        public void CreateRecipe(Recipe recipe) => Create(recipe);

        public void DeleteRecipe(Recipe recipe) => Delete(recipe);

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(rec => rec.Name).ToListAsync();

        public async Task<Recipe> GetRecipeAsync(Guid recipeId, bool trackChanges) =>
            await FindByCondition(rec => rec.Id.Equals(recipeId), trackChanges).SingleOrDefaultAsync();
    }
}
