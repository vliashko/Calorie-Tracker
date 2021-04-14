using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasData
            (
                new UserProfile
                {
                    Id = new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e"),
                    Login = "vlyashko02",
                    Weight = 84.2,
                    Height = 175,
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(2002, 04, 05)
                }
            );
        }
    }
}
