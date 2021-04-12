using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
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
                    Name = "Картошка",
                    Calories = 77,
                    Proteins = 2,
                    Fats = 0.4,
                    Carbohydrates = 16.3
                },
                new Ingredient
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "Макароны",
                    Calories = 98,
                    Proteins = 3.6,
                    Fats = 0.4,
                    Carbohydrates = 20
                },
                new Ingredient
                {
                    Id = new Guid("a1d8448e-b995-4783-b9d3-987c857c8c5d"),
                    Name = "Куриная грудка",
                    Calories = 113,
                    Proteins = 23.6,
                    Fats = 1.9,
                    Carbohydrates = 0.4
                }
            );
        }
    }
}
