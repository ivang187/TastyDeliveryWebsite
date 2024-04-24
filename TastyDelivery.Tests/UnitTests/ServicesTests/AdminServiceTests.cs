using Microsoft.AspNetCore.Identity;
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
        public void CreateRestaurant_ReturnsRestaurantWithSpecifiedDetails()
        {
            string name = "Test Restaurant";
            string workingHours = "8:00-17:00";
            string location = "123 Main St";

            var model = new AddRestaurantFormViewModel
            {
                Name = name,
                WorkingHours = workingHours,
                Location = location
            };

            adminService.CreateRestaurant(model);

            repository.Verify(r => r.AddNew(It.IsAny<Restaurant>));
            repository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Test]
        public void CreateProduct_Returns_ProductRestaurants()
        {
            int restaurantId = 1;
            string name = "Test Product";
            string description = "Test Description";
            ProductCategory category = ProductCategory.Mains;
            double price = 12.99;

            var productRestaurants = new ProductsRestaurants
            {
                ProductId = 1,
                RestaurantId = restaurantId,

            };

            var result = adminService.CreateProduct(restaurantId, productRestaurants.ProductId, name, description, category, price);


            Assert.IsNotNull(result);
            Assert.That(result.RestaurantId, Is.EqualTo(restaurantId));
            Assert.That(result.Product.Name, Is.EqualTo(name));
            Assert.That(result.Product.Description, Is.EqualTo(description));
            Assert.That(result.Product.Category, Is.EqualTo(category));
            Assert.That(result.Price, Is.EqualTo(price));
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
            repository.Verify(r => r.SaveChanges(), Times.AtLeast(2));
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

    }
}
