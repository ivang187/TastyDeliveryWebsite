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
    internal class ProductRestaurantsConfiguration : IEntityTypeConfiguration<ProductsRestaurants>
    {
        public void Configure(EntityTypeBuilder<ProductsRestaurants> builder)
        {
            builder.HasData
                (
                    new ProductsRestaurants
                    {
                        ProductId = 1,
                        RestaurantId = 1,
                        Price = 10.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 1,
                        RestaurantId = 3,
                        Price = 9.70
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 2,
                        RestaurantId = 1,
                        Price = 11.90
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 2,
                        RestaurantId = 3,
                        Price = 10.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 3,
                        RestaurantId = 1,
                        Price = 15.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 3,
                        RestaurantId = 2,
                        Price = 15.60
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 3,
                        RestaurantId = 3,
                        Price = 15.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 4,
                        RestaurantId = 2,
                        Price = 9.10
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 4,
                        RestaurantId = 3,
                        Price = 8.00
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 5,
                        RestaurantId = 1,
                        Price = 4.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 5,
                        RestaurantId = 2,
                        Price = 4.70
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 5,
                        RestaurantId = 3,
                        Price = 4.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 6,
                        RestaurantId = 3,
                        Price = 4.90
                    }, 
                    new ProductsRestaurants
                    {
                        ProductId = 7,
                        RestaurantId = 2,
                        Price = 6.20
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 7,
                        RestaurantId = 3,
                        Price = 5.00
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 8,
                        RestaurantId = 1,
                        Price = 9.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 8,
                        RestaurantId = 3,
                        Price = 9.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 9,
                        RestaurantId = 2,
                        Price = 7.90
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 10,
                        RestaurantId = 1,
                        Price = 11.90
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 10,
                        RestaurantId = 2,
                        Price = 12.40
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 10,
                        RestaurantId = 3,
                        Price = 10.80
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 11,
                        RestaurantId = 1,
                        Price = 14.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 11,
                        RestaurantId = 2,
                        Price = 14.90
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 11,
                        RestaurantId = 3,
                        Price = 11.80
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 12,
                        RestaurantId = 2,
                        Price = 3.30
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 12,
                        RestaurantId = 3,
                        Price = 2.50
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 13,
                        RestaurantId = 2,
                        Price = 10.80
                    },
                    new ProductsRestaurants
                    {
                        ProductId = 13,
                        RestaurantId = 3,
                        Price = 8.80
                    }
                );
        }
    }
}
