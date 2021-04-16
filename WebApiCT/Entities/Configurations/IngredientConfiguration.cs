using CaloriesTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CaloriesTracker.Entities.Configurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasData
            (
                new Ingredient
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "Potato",
                    Calories = 77f,
                    Proteins = 2f,
                    Fats = 0.4f,
                    Carbohydrates = 16.3f
                },
                new Ingredient
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "Pasta",
                    Calories = 98f,
                    Proteins = 3.6f,
                    Fats = 0.4f,
                    Carbohydrates = 20f
                },
                new Ingredient
                {
                    Id = new Guid("a1d8448e-b995-4783-b9d3-987c857c8c5d"),
                    Name = "Chicken breast",
                    Calories = 113f,
                    Proteins = 23.6f,
                    Fats = 1.9f,
                    Carbohydrates = 0.4f
                }
            );
        }
    }
}
