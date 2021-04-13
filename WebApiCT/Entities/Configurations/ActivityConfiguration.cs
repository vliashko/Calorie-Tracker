using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasData
            (
                new Activity
                {
                    Id = new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"),
                    Name = "Утреняя тренировка",
                    Start = new DateTime(2021, 04, 13, 8, 30, 0),
                    Finish = new DateTime(2021, 04, 13, 8, 50, 0)
                }
            );
        }
    }
}
