using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using TastyDelivery.Tests.Mocks;

namespace TastyDelivery.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected TastyDeliveryDbContext context;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            context = DatabaseMock.Instance;
            SeedDatabase();
        }

        public ApplicationUser Customer { get; private set; }

        public ApplicationUser DeliveryMan { get; private set; }
        public Order Order { get; private set; }

        public Product Product { get; private set; }

        public Restaurant Restaurant { get; private set; }

        public OrderProducts OrderProducts { get; private set; }

        public ProductsRestaurants ProductsRestaurants { get; private set; }
        private void SeedDatabase()
        {
            Customer = new ApplicationUser()
            { 
                Id = "fsfwerw0fwew-wg0923r23fwdsdfs",
                Email = "ivan187@gmail.com",
                FirstName = "Ivan",
                LastName = "Georgiev",
                Role = UserRole.Customer
            };

            context.Users.Add(Customer);

            DeliveryMan = new ApplicationUser()
            {
                Id = "duwefhiwfjasdkfasf-qwqwrqf",
                Email = "ivan12345@gmail.com",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Role = UserRole.DeliveryMan
            };


            context.Users.Add(Customer);


            Order = new Order()
            {
                Id = 6,
                TotalPrice = 22.20,
                UserId = "fsfwerw0fwew-wg0923r23fwdsdfs",
                DeliveryManId = "duwefhiwfjasdkfasf-qwqwrqf",
                HomeAddress = "Polqna 1, Samokov"
            };
            
            context.Orders.Add(Order);

            Product = new Product()
            {
                Id = 5,
                Name = "Grilled Trout",
                Description = "200g",
                Category = ProductCategory.Mains
            };

            context.Products.Add(Product);

            Restaurant = new Restaurant()
            {
                Id = 2,
                Name = "Sote",
                Location = "Gradina 2, Samokov",
                WorkingHours = "8:00-23:00"
            };

            context.Restaurants.Add(Restaurant);

            OrderProducts = new OrderProducts()
            {
                OrderId = 6,
                Order = Order,
                Product = Product,
                ProductId = 5,
            };

            context.OrderProducts.Add(OrderProducts);

            ProductsRestaurants = new ProductsRestaurants()
            {
                Price = 12.25,
                Product = Product,
                ProductId = 5,
                Restaurant = Restaurant,
                RestaurantId = 2,
            };

            context.ProductsRestaurants.Add(ProductsRestaurants);

            context.SaveChanges();  
        }

        [OneTimeTearDown]
        public void TearDownBase()
        {
            context.Dispose();
        }
    }
}
