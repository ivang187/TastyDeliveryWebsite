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

            var restaurant = adminService.CreateRestaurant(name, workingHours, location);

            Assert.IsNotNull(restaurant);
            Assert.That(restaurant.Name, Is.EqualTo(name));
            Assert.That(restaurant.WorkingHours, Is.EqualTo(workingHours));
            Assert.That(restaurant.Location, Is.EqualTo(location));
        }

        [Test]
        public void CreateProduct_Returns_ProductRestaurants()
        {
            int restaurantId = 1;
            string name = "Test Product";
            string description = "Test Description";
            ProductCategory category = ProductCategory.Mains;
            double price = 12.99;

            var result = adminService.CreateProduct(restaurantId, name, description, category, price);


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
            repository.Setup(r => r.SaveChanges()).Verifiable();
            repository.Setup(r => r.AddNew(It.IsAny<ApplicationUser>())).Verifiable();

            await adminService.CreateDriver(model);

            repository.Verify(r => r.AddNew(It.IsAny<ApplicationUser>()), Times.Once);
            repository.Verify(r => r.SaveChanges(), Times.Once);
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
            };

            userManager.Setup(u => u.FindByEmailAsync(model.Email)).ReturnsAsync(existingUser);
            repository.Setup(r => r.Update(It.IsAny<ApplicationUser>())).Verifiable();

            await adminService.CreateDriver(model);

            repository.Verify(r => r.Update(It.IsAny<ApplicationUser>()), Times.Once);
            repository.Verify(r => r.SaveChanges(), Times.Once);
        }

    }
}
