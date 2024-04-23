using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Infrastructure.Data.SeedData
{
    internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder) 
        {
            builder.HasData(
                new Restaurant
                {
                    Id = 1,
                    Name = "Mishel",
                    WorkingHours = "8:00-23:00",
                    Location = "Iskar Boulevard 65, Samokov",
                    Type = "Bar & Dinner"
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Pri Sote",
                    WorkingHours = "8:00-0:00",
                    Location = "Tourist Garden Park 82, Samokov",
                    Type = "Mehana"
                },
                new Restaurant
                {
                    Id = 3,
                    Name = "Delight",
                    WorkingHours = "8:00-23:00",
                    Location = "Tsar Boris III Boulevard 127A, Samokov",
                    Type = "Bar & Dinner"
                }
                );
        }
    }
}
