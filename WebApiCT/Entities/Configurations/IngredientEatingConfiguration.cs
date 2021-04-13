using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class IngredientEatingConfiguration : IEntityTypeConfiguration<IngredientEating>
    {
        public void Configure(EntityTypeBuilder<IngredientEating> builder)
        {
            builder.HasData
            (
                new IngredientEating
                {
                    Id = new Guid("3e687ead-a71c-4ea3-9bd4-d8596e6a5339"),
                    EatingId = new Guid("9a91cf0c-7b9a-43ea-b87e-95e1dd30354e"),
                    IngredientId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Grams = 300
                },
                new IngredientEating
                {
                    Id = new Guid("5ef0d854-3d0f-4b99-bff0-4545a236236a"),
                    EatingId = new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"),
                    IngredientId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Grams = 200
                },
                new IngredientEating
                {
                    Id = new Guid("f71f6aa0-a6a3-49d7-8cb9-a7e0b7c261b9"),
                    EatingId = new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"),
                    IngredientId = new Guid("a1d8448e-b995-4783-b9d3-987c857c8c5d"),
                    Grams = 100
                }
            );
        }
    }
}
