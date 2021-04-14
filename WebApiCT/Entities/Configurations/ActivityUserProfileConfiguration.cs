using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class ActivityUserProfileConfiguration : IEntityTypeConfiguration<ActivityUserProfile>
    {
        public void Configure(EntityTypeBuilder<ActivityUserProfile> builder)
        {
            builder.HasData
            (
                new ActivityUserProfile
                {
                    Id = new Guid("d3f8f77c-089e-425e-b79e-eb329456463c"),
                    ActivityId = new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"),
                    UserProfileId = new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e")
                }
            );
        }
    }
}
