using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Services;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;

namespace TastyDelivery.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class RestaurantServiceTests : UnitTestsBase
    {
        private Mock<IRepository> mockRepository;
        private RestaurantService restaurantService;

        [OneTimeSetUp]
        public void SetUp()
        {
            mockRepository = new Mock<IRepository>();
            restaurantService = new RestaurantService(mockRepository.Object);
        }

        [Test]
        public void GetRestaurantName_ReturnsName()
        {
            int id = 1;
            string expectedName = "Test Restaurant";
            var mockRestaurant = new Restaurant { Id = id, Name = expectedName };
            var mockQueryable = new List<Restaurant> { mockRestaurant }.AsQueryable();

            mockRepository.Setup(r => r.AllReadOnly<Restaurant>()).Returns(mockQueryable);

            var result = restaurantService.GetRestaurantName(id);

            Assert.That(result, Is.EqualTo(expectedName));
        }

        [Test]
        public void GetRestaurantMenu_ReturnsMenuItems()
        {
            int restaurantId = 1;
            var mockProductsRestaurants = new List<ProductsRestaurants>
            {
                new ProductsRestaurants
                {
                    RestaurantId = restaurantId,
                    Restaurant = new Restaurant { Id = restaurantId, Name = "Test Restaurant" },
                    ProductId = 1,
                    Product = new Product { Id = 1, Name = "Test Product", Description = "Description", Category = ProductCategory.Mains },
                    Price = 10.0
                }
            }.AsQueryable();

            mockRepository.Setup(r => r.AllReadOnly<ProductsRestaurants>()).Returns(mockProductsRestaurants);

            var result = restaurantService.GetRestaurantMenu(restaurantId);

            Assert.IsNotNull(result);

            var menuItems = result.ToList();
            Assert.That(menuItems, Is.Not.Null); 

            Assert.That(menuItems.Count, Is.EqualTo(1)); 
            Assert.That(menuItems[0].RestaurantId, Is.EqualTo(restaurantId));
            Assert.That(menuItems[0].RestaurantName, Is.EqualTo("Test Restaurant"));
            Assert.That(menuItems[0].ProductId, Is.EqualTo(1));
            Assert.That(menuItems[0].ProductName, Is.EqualTo("Test Product"));
            Assert.That(menuItems[0].Description, Is.EqualTo("Description"));
            Assert.That(menuItems[0].Price, Is.EqualTo(10.0));
            Assert.That(menuItems[0].Category, Is.EqualTo(ProductCategory.Mains));
        }
    }
}
