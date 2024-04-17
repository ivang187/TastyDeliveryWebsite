using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.DeliveryManModels;
using TastyDelivery.Core.Services;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class DeliveryManServiceTests : UnitTestsBase
    {
        private Mock<IRepository> repository;
        private DeliveryManService service;
        private Mock<UserManager<ApplicationUser>> userManager;
        private ApplicationUser mockUser;

        [OneTimeSetUp]
        public void SetUp()
        {
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

            repository = new Mock<IRepository>();
            userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            service = new DeliveryManService(repository.Object, userManager.Object);
        }

        [Test]
        public async Task AssignOrderToWorker_AssignsOrderToWorker()
        {
            // Arrange
            int orderId = 1;
            string userId = "1";

            var order = new Order
            {
                Id = orderId,
                UserId = userId,
                User = mockUser,
                HomeAddress = "fsdfsgdgd",
                PhoneNumber = "FSDGSDGADSF",
                TotalPrice = 12.21,
                Status = DeliveryStatus.Pending,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "Sote" }
            };

            repository.Setup(r => r.AllReadOnly<Order>()).Returns(new List<Order>() { order } .AsQueryable());
            userManager.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(mockUser);
            
            // Act
            await service.AssignOrderToWorker(orderId, userId);

            // Assert
            repository.Verify(r => r.Update(order));
            repository.Verify(r => r.SaveChanges());
            Assert.That(order.Status, Is.EqualTo(DeliveryStatus.OutForDelivery));
        }

        [Test]
        public void GetPendingDeliveries_ReturnsOrderDetailsViewModelList_WhenPendingOrdersExist()
        {
            var order = new Order
            {
                Id = 1,
                UserId = mockUser.Id,
                User = mockUser,
                HomeAddress = "fsdfsgdgd",
                PhoneNumber = "FSDGSDGADSF",
                TotalPrice = 12.21,
                Status = DeliveryStatus.Pending,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "Sote" }
            };

            var mockProduct = new Product { Id = 1, Name = "Test Product" };
            var mockRestaurant = new Restaurant { Id = 1, Name = "TestRes" };
            
            order.Products = new List<OrderProducts> { new OrderProducts { Product = mockProduct } };

            repository.Setup(r => r.AllReadOnly<Order>())
                          .Returns(new List<Order> { order }.AsQueryable());
            repository.Setup(r => r.AllReadOnly<ApplicationUser>())
                .Returns(new List<ApplicationUser> { mockUser }.AsQueryable());
            repository.Setup(r => r.AllReadOnly<Restaurant>())
                .Returns(new List<Restaurant> { mockRestaurant }.AsQueryable());

            var result = service.GetPendingDeliveries();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].OrderId, Is.EqualTo(order.Id));
        }

        [Test]
        public void CreateAssignedOrderModel_SuccessfullyCreatesAnAssignedOrderModel()
        {
            var deliveryManMock = new ApplicationUser
            {
                Id = "2",
                FirstName = "Jay",
                LastName = "Doe",
                PhoneNumber = "987654321",
                Email = "ivan2@gmail.com",
                HomeAddress = "1234 St.",
                Role = UserRole.DeliveryMan,
                UserName = "ivan@gmail.com"
            };

            var order = new Order
            {
                Id = 1,
                UserId = mockUser.Id,
                User = mockUser,
                DeliveryMan = deliveryManMock,
                DeliveryManId = deliveryManMock.Id,
                HomeAddress = "fsdfsgdgd",
                PhoneNumber = "FSDGSDGADSF",
                TotalPrice = 12.21,
                Status = DeliveryStatus.OutForDelivery,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "Sote" }
            };

            var mockRestaurant = new Restaurant { Id = 1, Name = "TestRes" };

            repository.Setup(r => r.AllReadOnly<Order>())
                .Returns(new List<Order> { order }.AsQueryable());
            repository.Setup(r => r.AllReadOnly<ApplicationUser>())
                .Returns(new List<ApplicationUser> { mockUser, deliveryManMock }.AsQueryable());
            repository.Setup(r => r.AllReadOnly<Restaurant>())
                .Returns(new List<Restaurant> { mockRestaurant }.AsQueryable());

            var result = service.CreateAssignedOrderModel(order.Id);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<AssignedOrdersViewModel>());
            Assert.That(result.User, Is.EqualTo(mockUser));
            Assert.That(result.Address, Is.EqualTo("fsdfsgdgd"));
            Assert.That(result.PhoneNumber, Is.EqualTo("FSDGSDGADSF"));
            Assert.That(result.TotalPrice, Is.EqualTo(12.21));
            Assert.That(result.Order.Status, Is.EqualTo(DeliveryStatus.OutForDelivery));
            Assert.That(result.DeliveryMan.Email, Is.EqualTo("ivan2@gmail.com"));
        }

        [Test]
        public void GetAssignedOrders_ReturnsListOfAssignedOrderModel()
        {
            var deliveryManMock = new ApplicationUser
            {
                Id = "2",
                FirstName = "Jay",
                LastName = "Doe",
                PhoneNumber = "987654321",
                Email = "ivan2@gmail.com",
                HomeAddress = "1234 St.",
                Role = UserRole.DeliveryMan,
                UserName = "ivan@gmail.com"
            };

            var order = new Order
            {
                Id = 1,
                UserId = mockUser.Id,
                User = mockUser,
                DeliveryMan = deliveryManMock,
                DeliveryManId = deliveryManMock.Id,
                HomeAddress = "fsdfsgdgd",
                PhoneNumber = "FSDGSDGADSF",
                TotalPrice = 12.21,
                Status = DeliveryStatus.OutForDelivery,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "Sote" }
            };

            var mockRestaurant = new Restaurant { Id = 1, Name = "TestRes" };

            repository.Setup(r => r.AllReadOnly<Order>())
                .Returns(new List<Order> { order }.AsQueryable());
            repository.Setup(r => r.AllReadOnly<ApplicationUser>())
                .Returns(new List<ApplicationUser> { mockUser, deliveryManMock }.AsQueryable());
            repository.Setup(r => r.AllReadOnly<Restaurant>())
                .Returns(new List<Restaurant> { mockRestaurant }.AsQueryable());

            var result = service.GetAssignedOrders(deliveryManMock.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<List<AssignedOrdersViewModel>>());
        }

        [Test]
        public void DeliverOrder_SuccessfullyChangesDeliveryStatus()
        {
            var order = new Order
            {
                Id = 1,
                Status = DeliveryStatus.OutForDelivery
            };

            repository.Setup(r => r.AllReadOnly<Order>())
                .Returns(new List<Order> { order }.AsQueryable());

            service.DeliverOrder(order.Id);

            repository.Verify(r => r.Update(order));
            repository.Verify(r => r.SaveChanges());
            Assert.That(order.Status, Is.EqualTo(DeliveryStatus.Delivered));
        }
    }
}
