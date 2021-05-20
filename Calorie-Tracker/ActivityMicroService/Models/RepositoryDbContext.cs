using Microsoft.EntityFrameworkCore;

namespace ActivityMicroService.Models
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityExercise> ActivityExercises { get; set; }
    }
}
