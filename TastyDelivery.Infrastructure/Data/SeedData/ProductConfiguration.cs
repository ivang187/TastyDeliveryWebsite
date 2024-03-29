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
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
                (
                    new Product
                    {
                        Id = 1,
                        Name = "Chicken Burger",
                        Description = "Crispy Chicken Fillet, Tomato, Iceberg Lettuce, Mayonnaise, Burger Bun, Fries",
                        Category = Models.Enums.ProductCategory.Mains
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Beef Burger",
                        Description = "Beef Patty, Pickles, Caramelized Onion, Sauce, Burger Bun, Fries",
                        Category = Models.Enums.ProductCategory.Mains
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Pork Schnitzel with Fries",
                        Description = "300g",
                        Category = Models.Enums.ProductCategory.Mains
                    },
                    new Product
                    {
                        Id = 4,
                        Name = "Shopska Salad",
                        Description = "Fresh Tomatoes, Cucumber, Bell Peppers, Feta Cheese, Olives",
                        Category = Models.Enums.ProductCategory.Salad
                    },
                    new Product
                    {
                        Id = 5,
                        Name = "Chicken Soup",
                        Description = "300ml",
                        Category = Models.Enums.ProductCategory.Soup
                    },
                    new Product
                    {
                        Id = 6,
                        Name = "Cheesecake",
                        Description = "120g",
                        Category = Models.Enums.ProductCategory.Desert
                    },
                    new Product
                    {
                        Id = 7,
                        Name = "Shkembe Chorba",
                        Description = "300ml",
                        Category = Models.Enums.ProductCategory.Soup
                    },
                    new Product
                    {
                        Id = 8,
                        Name = "Pasta Carbonara",
                        Description = "Pasta, Pancetta, Parmesan",
                        Category = Models.Enums.ProductCategory.Mains
                    },
                    new Product
                    {
                        Id = 9,
                        Name = "Chocolate Cake",
                        Description = "100g",
                        Category = Models.Enums.ProductCategory.Desert
                    },
                    new Product
                    {
                        Id = 10,
                        Name = "Caesar Salad",
                        Description = "Romaine Lettuce, Parmesan Cheese, Croutons, Dressing",
                        Category = Models.Enums.ProductCategory.Salad
                    },
                    new Product
                    {
                        Id = 11,
                        Name = "Grilled Trout",
                        Description = "200g",
                        Category = Models.Enums.ProductCategory.Mains
                    },
                    new Product
                    {
                        Id = 12,
                        Name = "Meatball",
                        Description = "100g",
                        Category = Models.Enums.ProductCategory.Mains
                    },
                    new Product
                    {
                        Id = 13,
                        Name = "Chicken bites with Cornflakes",
                        Description = "150g",
                        Category = Models.Enums.ProductCategory.Mains
                    }
                ); 
        }
    }
}
