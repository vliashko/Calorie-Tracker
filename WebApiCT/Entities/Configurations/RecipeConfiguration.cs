using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasData
            (
                new Recipe
                {
                    Id = new Guid("000c0477-d0ec-472d-b65c-1b3561dac2a0"),
                    UserId = new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e"),
                    Name = "Картошка с курицей",
                    Description = "Также просто, как и макароны, но необычно"
                }
            );
        }
    }
}
