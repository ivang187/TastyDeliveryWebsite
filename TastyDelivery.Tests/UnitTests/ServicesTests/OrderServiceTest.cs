using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.OrderModels;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using TastyDelivery.Models.RestaurantModels;

namespace TastyDelivery.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class OrderServiceTest : UnitTestsBase
    {

        private Mock<IRepository> repository;
        private OrderService orderService;
        private ApplicationUser mockUser;
        private Restaurant mockRestaurant;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository>();

            mockUser = new ApplicationUser
            {
                Id = "1",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                Email = "ivan@gmail.com",
                HomeAddress = "123 St.",
                Role = UserRole.Customer,
                UserName = "ivan@gmail.com",
                Orders = new List<Order>()
            };

            mockRestaurant = new Restaurant { Id = 1, Name = "Test Restaurant" };

            repository.Setup(r => r.AllReadOnly<ApplicationUser>())
                .Returns(new List<ApplicationUser> { mockUser }.AsQueryable());

            orderService = new OrderService(repository.Object);
        }

        [Test]
        public async Task TestCreateOrder()
        {
            var model = new CheckoutViewModel
            {
                User = mockUser,
                FirstName = mockUser.FirstName,
                LastName = mockUser.LastName,
                Phone = mockUser.PhoneNumber,
                Address = "123 St.",
                Products = new List<CartItemViewModel> { new CartItemViewModel { Id = 1, Price = 10, Quantity = 2 } },
                Restaurant = new Restaurant { Id = 1, Name = "Test Restaurant", Location = "145 St.", WorkingHours = "8:00-15:00" }
            };

            repository.Setup(r => r.AllReadOnly<Restaurant>()).Returns(new List<Restaurant> { mockRestaurant }.AsQueryable());

            var result = await orderService.CreateOrder(model);

            Assert.IsNotNull(result);
            Assert.That(result.HomeAddress, Is.EqualTo("123 St."));
            Assert.That(result.TotalPrice, Is.EqualTo(20));
            Assert.That(result.Status, Is.EqualTo(DeliveryStatus.Pending));
            Assert.That(result.UserId, Is.EqualTo("1"));
            Assert.IsNotNull(result.User);
            Assert.That(result.User.FirstName, Is.EqualTo("John"));
            Assert.That(result.User.LastName, Is.EqualTo("Doe"));
            Assert.That(result.User.PhoneNumber, Is.EqualTo("123456789"));
            Assert.That(result.RestaurantId, Is.EqualTo(1));

            repository.Verify(r => r.AddNew(It.IsAny<Order>()));
            repository.Verify(r => r.SaveChanges());
        }


        [Test]
        public void GetUserOrders_ReturnsOrders_WhenUserHasOrders()
        {
            // Arrange

            var mockOrders = new List<Order> 
            { 
                new Order 
                {
                    Id = 1, UserId = "1", PhoneNumber = "123456789", HomeAddress = "123 Main St", TotalPrice = 50.0, Status = DeliveryStatus.Pending, Products = new List<OrderProducts>() 
                },
                new Order
                {
                    Id = 2, UserId = "1", PhoneNumber = "123456789", HomeAddress = "123 Main St", TotalPrice = 20.0, Status = DeliveryStatus.OutForDelivery, Products = new List<OrderProducts>()
                },
                new Order
                {
                    Id = 3, UserId = "1", PhoneNumber = "123456789", HomeAddress = "123 Main St", TotalPrice = 50.0, Status = DeliveryStatus.Pending, Products = new List<OrderProducts>()
                }
            };

            repository.Setup(r => r.AllReadOnly<Order>())
                          .Returns(mockOrders.AsQueryable());

            // Act
            var result = orderService.GetUserOrders(mockUser);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(3));

            var firstOrder = result[0];
            Assert.That(firstOrder.OrderId, Is.EqualTo(mockOrders[0].Id));
            Assert.That(firstOrder.FullName, Is.EqualTo(mockUser.FirstName + ' ' + mockUser.LastName));
            Assert.That(firstOrder.PhoneNumber, Is.EqualTo(mockOrders[0].PhoneNumber));
            Assert.That(firstOrder.Address, Is.EqualTo(mockOrders[0].HomeAddress));
            Assert.That(firstOrder.TotalPrice, Is.EqualTo(mockOrders[0].TotalPrice));
            Assert.That(firstOrder.Status, Is.EqualTo(mockOrders[0].Status));

            Assert.IsNotNull(firstOrder.Products);
            Assert.That(firstOrder.Products.Count, Is.EqualTo(mockOrders[0].Products.Count));
        }

        [Test]
        public void GetUserOrders_ReturnsNullWhenNoOrders()
        {
            repository.Setup(r => r.AllReadOnly<Order>()).Returns(new List<Order>().AsQueryable());

            var result = orderService.GetUserOrders(mockUser);

            Assert.IsNull(result);
        }

        [Test]
        public void GetUserOrders_ReturnsMultipleOrders()
        {
            var mockOrders = new List<Order>
            {
                new Order()
                {
                Id = 1,
                User = mockUser,
                UserId = mockUser.Id,
                Status = DeliveryStatus.OutForDelivery,
                HomeAddress = "sdfsdfsdf",
                PhoneNumber = "|FDFSDF",
                TotalPrice = 44.02
                },
                new Order()
                {
                Id = 2,
                User = mockUser,
                UserId = mockUser.Id,
                Status = DeliveryStatus.Pending,
                HomeAddress = "sdsdgsd",
                PhoneNumber = "|FDFSDF",
                TotalPrice = 42.02
                }
            };
            

            repository.Setup(r => r.AllReadOnly<Order>())
                .Returns(mockOrders.AsQueryable());

            var result = orderService.GetUserOrders(mockUser);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetUserOrders_ReturnsOrderDetailsViewModelList_WhenUserHasOrders()
        {
            // Arrange
            var user = new ApplicationUser { Id = "1" };
            var orders = new List<Order>
            {
                new Order { UserId = "1", Status = DeliveryStatus.Pending },
                new Order { UserId = "1", Status = DeliveryStatus.OutForDelivery }
            };

            repository.Setup(repo => repo.AllReadOnly<Order>())
                          .Returns(orders.AsQueryable());

            // Act
            var result = orderService.GetUserOrders(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.IsTrue(result.All(order => order.User.Id == user.Id));
        }
    }
}
