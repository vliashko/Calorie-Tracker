using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasData
            (
                new Exercise
                {
                    Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                    Name = "Pull-ups",
                    Description = "Performed on the crossbar. Duration 40 seconds",
                    CaloriesSpent = 5,
                },
                new Exercise
                {
                    Id = new Guid("291bf3d3-9c56-4f6c-b78e-9b100a2e7b55"),
                    Name = "Squats",
                    Description = "From a standing position, feet shoulder width apart",
                    CaloriesSpent = 10,
                }
            );
        }
    }
}
