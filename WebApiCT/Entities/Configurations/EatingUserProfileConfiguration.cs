using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class EatingUserProfileConfiguration : IEntityTypeConfiguration<EatingUserProfile>
    {
        public void Configure(EntityTypeBuilder<EatingUserProfile> builder)
        {
            builder.HasData
            (
                new EatingUserProfile
                {
                    Id = new Guid("1b3039d0-7372-47d8-bff2-5205bf580c39"),
                    UserProfileId = new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e"),
                    EatingId = new Guid("9a91cf0c-7b9a-43ea-b87e-95e1dd30354e")
                },
                new EatingUserProfile
                {
                    Id = new Guid("10ec2edc-e38c-40b1-a83f-216c1992a457"),
                    UserProfileId = new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e"),
                    EatingId = new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8")
                }
            );
        }
    }
}
