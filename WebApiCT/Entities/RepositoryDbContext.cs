using Entities.Configurations;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
