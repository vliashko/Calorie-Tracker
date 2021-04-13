using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class IngredientRecipeConfiguration : IEntityTypeConfiguration<IngredientRecipe>
    {
        public void Configure(EntityTypeBuilder<IngredientRecipe> builder)
        {
            builder.HasData
            (
                new IngredientRecipe
                {
                    Id = new Guid("2fc02eb0-b6dd-46e7-aefc-d71f14b5ecbd"),
                    RecipeId = new Guid("000c0477-d0ec-472d-b65c-1b3561dac2a0"),
                    IngredientId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Grams = 200
                },
                new IngredientRecipe
                {
                    Id = new Guid("2fc02eb0-b6dd-46e7-aefc-d71f14b5ecdb"),
                    RecipeId = new Guid("000c0477-d0ec-472d-b65c-1b3561dac2a0"),
                    IngredientId = new Guid("a1d8448e-b995-4783-b9d3-987c857c8c5d"),
                    Grams = 100
                }
            );
        }
    }
}
