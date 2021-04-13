using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class ActivityExerciseConfiguration : IEntityTypeConfiguration<ActivityExercise>
    {
        public void Configure(EntityTypeBuilder<ActivityExercise> builder)
        {
            builder.HasData
            (
                new ActivityExercise
                {
                    Id = new Guid("eac5d895-df3d-41aa-abcc-2915be0bb837"),
                    ActivityId = new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"),
                    ExerciseId = new Guid("291bf3d3-9c56-4f6c-b78e-9b100a2e7b55")
                },
                new ActivityExercise
                {
                    Id = new Guid("398f8ada-bf2a-491e-bce9-9cca15f45120"),
                    ActivityId = new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"),
                    ExerciseId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e")
                }
            );
        }
    }
}
