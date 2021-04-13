using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
                new User
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
