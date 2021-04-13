using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class EatingConfiguration : IEntityTypeConfiguration<Eating>
    {
        public void Configure(EntityTypeBuilder<Eating> builder)
        {
            builder.HasData
            (
                new Eating
                {
                    Id = new Guid("9a91cf0c-7b9a-43ea-b87e-95e1dd30354e"),
                    Name = "Завтрак",
                    Moment = new DateTime(2021, 04, 13, 8, 55, 0)
                },
                new Eating
                {
                    Id = new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"),
                    Name = "Обед",
                    Moment = new DateTime(2021, 04, 13, 13, 0, 0)
                }
            );
        }
    }
}
