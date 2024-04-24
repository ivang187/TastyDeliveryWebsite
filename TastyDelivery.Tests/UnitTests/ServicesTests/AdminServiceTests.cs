using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.AdminModels;
using TastyDelivery.Core.Services;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Tests.UnitTests.ServicesTests
{
    public class AdminServiceTests
    {
        private Mock<IRepository> repository;
        private Mock<UserManager<ApplicationUser>> userManager;
        private AdminService adminService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository>();
            userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            adminService = new AdminService(repository.Object, userManager.Object);
        }


        [Test]
        public async Task CreateDriver_WithNewUser_CreatesNewDriver()
        {
            // Arrange
            var model = new AppointDriverModel
            {
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "Testov",
                Password = "Password",
                ConfirmPassword = "Confirm Password",
                PhoneNumber = "1234567890"
            };

            userManager.Setup(u => u.FindByEmailAsync(model.Email)).ReturnsAsync((ApplicationUser)null);

            await adminService.CreateDriver(model);

            repository.Verify(r => r.AddNew(It.IsAny<ApplicationUser>()));
            repository.Verify(r => r.SaveChanges());
        }

        [Test]
        public async Task CreateDriver_WithExistingUser_UpdatesUserRole()
        {
            var model = new AppointDriverModel
            {
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "Testov",
                Password = "Password",
                ConfirmPassword = "Confirm Password",
                PhoneNumber = "1234567890"
            };

            ApplicationUser existingUser = new ApplicationUser
            {
                Id = "dsggsfsaads",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "Testov",
                HomeAddress = "123 St.",
                PhoneNumber = "1234567890",
                UserName = "text@example.com",
                Role = UserRole.Customer
            };

            userManager.Setup(u => u.FindByEmailAsync(model.Email)).ReturnsAsync(existingUser);
            repository.Setup(r => r.Update(It.IsAny<ApplicationUser>())).Verifiable();

            await adminService.CreateDriver(model);

            repository.Verify(r => r.Update(It.IsAny<ApplicationUser>()));
            repository.Verify(r => r.SaveChanges());
            Assert.That(existingUser.Role, Is.EqualTo(UserRole.DeliveryMan));
        }

        [Test]

        public void GetCompletedDeliveries_ReturnsCorrectInformation()
        {
            var mockUser = new ApplicationUser
            {
                Id = "1",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                Email = "ivan@gmail.com",
                HomeAddress = "123 St.",
                Role = UserRole.Customer,
                UserName = "ivan@gmail.com"
            };

            var deliveryManMock = new ApplicationUser
            {
                Id = "2",
                FirstName = "Jay",
                LastName = "Z",
                PhoneNumber = "987654321",
                Email = "ivan2@gmail.com",
                HomeAddress = "1234 St.",
                Role = UserRole.DeliveryMan,
                UserName = "ivan2@gmail.com"
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
                Status = DeliveryStatus.Delivered,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "TestRes" }
            };

            var order2 = new Order
            {
                Id = 2,
                UserId = mockUser.Id,
                User = mockUser,
                DeliveryMan = deliveryManMock,
                DeliveryManId = deliveryManMock.Id,
                HomeAddress = "fsdfsgdgd",
                PhoneNumber = "FSDGSDGADSF",
                TotalPrice = 14.50,
                Status = DeliveryStatus.Delivered,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "TestRes" }
            };

            var mockRestaurant = new Restaurant { Id = 1, Name = "TestRes" };

            repository.Setup(r => r.AllReadOnly<Order>())
                .Returns(new List<Order> { order, order2 }.AsQueryable());
            repository.Setup(r => r.AllReadOnly<ApplicationUser>())
                .Returns(new List<ApplicationUser> { mockUser, deliveryManMock }.AsQueryable());
            repository.Setup(r => r.AllReadOnly<Restaurant>())
                .Returns(new List<Restaurant> { mockRestaurant }.AsQueryable());

            var result = adminService.GetCompletedDeliveries();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf<List<CompletedDeliveriesAdminViewModel>>());
        }

        [Test]
        public void GetProductById_ReturnsCorrectProduct()
        {
            int productId = 1;
            var mockProduct = new Product { Id = productId, Name = "Test Product" };
            var mockRestaurant = new Restaurant { Id = 1, Name = "Test Restaurant" };
            var mockProductRestaurant = new ProductsRestaurants
            {
                ProductId = productId,
                Product = mockProduct,
                RestaurantId = mockRestaurant.Id,
                Restaurant = mockRestaurant
            };
            var mockQueryable = new[] { mockProductRestaurant }.AsQueryable();

            repository.Setup(r => r.AllReadOnly<ProductsRestaurants>())
                          .Returns(mockQueryable);

            var result = adminService.GetProductById(productId);

            Assert.IsNotNull(result);
            Assert.That(result.ProductId, Is.EqualTo(productId));
            Assert.That(result.Product.Name, Is.EqualTo(mockProduct.Name));
            Assert.That(result.Restaurant.Name, Is.EqualTo(mockRestaurant.Name));
        }

        [Test]
        public void DeleteProduct_MakesChangesInDb()
        {
            var mockProduct = new Product
            {
                Id = 1
            };

            var mockRestaurant = new Restaurant
            {
                Id = 2
            };

            var mockProductRestaurants = new ProductsRestaurants
            {
                Restaurant = mockRestaurant,
                RestaurantId = mockRestaurant.Id,
                Product = mockProduct,
                ProductId = mockProduct.Id,
                Price = 15.00
            };

            repository.Setup(r => r.Delete(mockProductRestaurants)).Verifiable();
            repository.Setup(r => r.SaveChanges()).Verifiable();

            adminService.DeleteProduct(mockProductRestaurants);

            repository.Verify(r => r.Delete(mockProductRestaurants));
            repository.Verify(r => r.SaveChanges());
        }

        [Test]
        public void GetPendingDeliveries_ReturnsPendingDeliveriesViewModels()
        {
            var mockUser = new ApplicationUser { Id = "1", FirstName = "John", LastName = "Doe" };
            var mockRestaurant = new Restaurant { Id = 1, Name = "Test Restaurant" };
            var mockOrder = new Order { Id = 1, TimeOrdered = DateTime.Now, User = mockUser, Restaurant = mockRestaurant, Status = DeliveryStatus.Pending };
            var mockOrders = new List<Order> { mockOrder };

            repository.Setup(r => r.AllReadOnly<Order>())
                          .Returns(mockOrders.AsQueryable());

            var result = adminService.GetPendingDeliveries();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));

            var pendingDelivery = result.First();
            Assert.That(pendingDelivery.OrderId, Is.EqualTo(mockOrder.Id));
            Assert.That(pendingDelivery.TimeOrdered, Is.EqualTo(mockOrder.TimeOrdered));
            Assert.That(pendingDelivery.Customer, Is.EqualTo(mockUser));
            Assert.That(pendingDelivery.CustomerName, Is.EqualTo($"{mockUser.FirstName} {mockUser.LastName}"));
            Assert.That(pendingDelivery.Restaurant, Is.EqualTo(mockRestaurant));
            Assert.That(pendingDelivery.RestaurantName, Is.EqualTo(mockRestaurant.Name));
        }
    }
}
