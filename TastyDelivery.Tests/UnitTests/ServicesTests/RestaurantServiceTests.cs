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
using TastyDelivery.Core.Models.AdminModels;
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

        [Test]
        public void GetAllRestaurants_ReturnsRestaurants()
        {
            var mockRestaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Restaurant1", Location = "Location1", WorkingHours = "10:00 - 18:00" },
                new Restaurant { Id = 2, Name = "Restaurant2", Location = "Location2", WorkingHours = "09:00 - 20:00" }
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(r => r.AllReadOnly<Restaurant>())
                          .Returns(mockRestaurants.AsQueryable());

            var restaurantService = new RestaurantService(mockRepository.Object);

            var result = restaurantService.GetAllRestaurants();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(mockRestaurants.Count));

            var expectedFirstRestaurant = mockRestaurants.First();
            var actualFirstRestaurant = result.First();
            Assert.That(actualFirstRestaurant.Id, Is.EqualTo(expectedFirstRestaurant.Id));
            Assert.That(actualFirstRestaurant.Name, Is.EqualTo(expectedFirstRestaurant.Name));
            Assert.That(actualFirstRestaurant.Location, Is.EqualTo(expectedFirstRestaurant.Location));
            Assert.That(actualFirstRestaurant.WorkingHours, Is.EqualTo(expectedFirstRestaurant.WorkingHours));

            var expectedLastRestaurant = mockRestaurants.Last();
            var actualLastRestaurant = result.Last();
            Assert.That(actualLastRestaurant.Id, Is.EqualTo(expectedLastRestaurant.Id));
            Assert.That(actualLastRestaurant.Name, Is.EqualTo(expectedLastRestaurant.Name));
            Assert.That(actualLastRestaurant.Location, Is.EqualTo(expectedLastRestaurant.Location));
            Assert.That(actualLastRestaurant.WorkingHours, Is.EqualTo(expectedLastRestaurant.WorkingHours));
        }

        [Test]
        public void DeleteRestaurant_MakesChangesInDb()
        {
            var mockRestaurant = new Restaurant
            {
                Id = 2
            };

            mockRepository.Setup(r => r.Delete(mockRestaurant)).Verifiable();
            mockRepository.Setup(r => r.SaveChanges()).Verifiable();

            restaurantService.Delete(mockRestaurant);

            mockRepository.Verify(r => r.Delete(mockRestaurant));
            mockRepository.Verify(r => r.SaveChanges());
        }

        [Test]
        public void GetRestaurantById_ReturnsRestaurant()
        {
            int restaurantId = 1;
            var expectedRestaurant = new Restaurant { Id = restaurantId, Name = "Test Restaurant" };
            var mockData = new List<Restaurant> { expectedRestaurant }.AsQueryable();

            mockRepository.Setup(r => r.AllReadOnly<Restaurant>())
                          .Returns(mockData);

            var result = restaurantService.GetRestaurantById(restaurantId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(expectedRestaurant));
        }

        [Test]
        public void CheckForPendingOrders_ReturnsTrue_IfPendingOrdersExist()
        {
            int restaurantId = 1;
            var mockOrders = new List<Order>
            {
                new Order { Id = 1, RestaurantId = restaurantId, Status = DeliveryStatus.Pending },
                new Order { Id = 2, RestaurantId = restaurantId, Status = DeliveryStatus.OutForDelivery },
                new Order { Id = 3, RestaurantId = restaurantId, Status = DeliveryStatus.OutForDelivery }
            };

            mockRepository.Setup(r => r.AllReadOnly<Order>())
                          .Returns(mockOrders.AsQueryable());

            var result = restaurantService.CheckForPendingOrders(restaurantId);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForPendingOrders_ReturnsFalse_IfNoPendingOrdersExist()
        {
            int restaurantId = 1;
            var mockOrders = new List<Order>
            {
                new Order { Id = 1, RestaurantId = restaurantId, Status = DeliveryStatus.Delivered },
                new Order { Id = 2, RestaurantId = restaurantId, Status = DeliveryStatus.OutForDelivery }
            };

            mockRepository.Setup(r => r.AllReadOnly<Order>())
                          .Returns(mockOrders.AsQueryable());

            var result = restaurantService.CheckForPendingOrders(restaurantId);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIfRestaurantExists_ReturnsTrue_IfRestaurantExists()
        {
            // Arrange
            string restaurantName = "Test Restaurant";
            var mockRestaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = restaurantName },
                new Restaurant { Id = 2, Name = "Other Restaurant" }
            };

            mockRepository.Setup(r => r.AllReadOnly<Restaurant>())
                          .Returns(mockRestaurants.AsQueryable());

            // Act
            var result = restaurantService.CheckIfRestaurantExists(restaurantName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIfRestaurantExists_ReturnsFalse_IfRestaurantDoesNotExist()
        {
            string restaurantName = "Test Restaurant";
            var mockRestaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Other Restaurant" },
                new Restaurant { Id = 2, Name = "Another Restaurant" }
            };

            mockRepository.Setup(r => r.AllReadOnly<Restaurant>())
                          .Returns(mockRestaurants.AsQueryable());

            var result = restaurantService.CheckIfRestaurantExists(restaurantName);

            Assert.IsFalse(result);
        }

        [Test]
        public void Update_UpdatesRestaurantProperties_AndSavesChanges()
        {
            var model = new AddRestaurantFormViewModel
            {
                Name = "Test Restaurant",
                WorkingHours = "10:00 - 18:00",
                Location = "Test Location",
                Type = "Test Type"
            };

            var mockRestaurant = new Restaurant
            {
                Id = 1,
                Name = model.Name,
                WorkingHours = "Old Working Hours",
                Location = "Old Location",
                Type = "Old Type"
            };

            mockRepository.Setup(r => r.AllReadOnly<Restaurant>())
                          .Returns(new List<Restaurant> { mockRestaurant }.AsQueryable());

            restaurantService.Update(model);

            Assert.That(mockRestaurant.WorkingHours, Is.EqualTo(model.WorkingHours));
            Assert.That(mockRestaurant.Location, Is.EqualTo(model.Location));
            Assert.That(mockRestaurant.Type, Is.EqualTo(model.Type));

            mockRepository.Verify(r => r.Update(mockRestaurant));
            mockRepository.Verify(r => r.SaveChanges());
        }

        [Test]
        public void GetDistinctTypes_ReturnsDistinctTypes()
        {
            // Arrange
            var mockRestaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Type = "Type1" },
                new Restaurant { Id = 2, Type = "Type2" },
                new Restaurant { Id = 3, Type = "Type1" },
                new Restaurant { Id = 4, Type = "Type3" }
            };

            mockRepository.Setup(r => r.AllReadOnly<Restaurant>())
                          .Returns(mockRestaurants.AsQueryable());

            var result = restaurantService.GetDistinctTypes();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(3)); 

            Assert.Contains("Type1", result);
            Assert.Contains("Type2", result);
            Assert.Contains("Type3", result);
        }
    }
}
