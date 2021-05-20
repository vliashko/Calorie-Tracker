using Microsoft.EntityFrameworkCore;

namespace EatingMicroService.Models
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Eating> Eatings { get; set; }
        public DbSet<IngredientEating> IngredientEatings { get; set; }
    }
}
