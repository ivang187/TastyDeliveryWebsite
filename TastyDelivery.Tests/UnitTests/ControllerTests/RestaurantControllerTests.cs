using Microsoft.AspNetCore.Mvc;
using Moq;
using TastyDelivery.Controllers;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.RestaurantModels;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Models.RestaurantModels;

namespace TastyDelivery.Tests.UnitTests.ControllerTests
{
    [TestFixture]
    public class RestaurantControllerTests : UnitTestsBase, IDisposable
    {
        private RestaurantController controller;
        private Mock<IRestaurantService> restaurantService;

        [OneTimeSetUp]
        public void SetUp()
        {
            restaurantService = new Mock<IRestaurantService>();
            controller = new RestaurantController(restaurantService.Object);
        }

        [Test]
        public void Restaurants_ReturnsViewWithModel()
        {
            var expectedModel = new List<RestaurantsViewModel>
            {
                new RestaurantsViewModel
                {   
                    Id = 1,
                    Name = "Test Restaurant",
                    Location = "123 St.",
                    WorkingHours = "8:00-14:00",
                    Type = "Test Type"
                }
            };

            var expectedType = "Test Type";

            restaurantService.Setup(s => s.GetAllRestaurants()).Returns(expectedModel);
            restaurantService.Setup(r => r.GetRestaurantsByType(expectedType)).Returns(expectedModel);

            var result = controller.Restaurants(expectedType) as ViewResult;

            Assert.IsNotNull(result);
            Assert.That(result.Model, Is.InstanceOf<IEnumerable<RestaurantsViewModel>>());

            var model = result.Model as IEnumerable<RestaurantsViewModel>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model, Contains.Item(expectedModel[0]));
        }

        [Test]
        public void ShowMenu_RedirectsToErrorAction_WhenMenuNotFound()
        {
            // Arrange
            restaurantService.Setup(service => service.GetRestaurantMenu(1))
                                  .Returns(new List<RestaurantMenuViewModel>()); 

            // Act
            var result = controller.ShowMenu(1);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.That(((RedirectToActionResult)result).ActionName, Is.EqualTo("Error"));
            Assert.That(((RedirectToActionResult)result).ControllerName, Is.EqualTo("Home"));
            Assert.That(((RedirectToActionResult)result).RouteValues?["statusCode"], Is.EqualTo(404));
        }

        [Test]
        public void ShowMenu_ReturnsViewResult_WhenMenuFound()
        {
            
            var mockMenuItems = new List<RestaurantMenuViewModel>
                {
                    new RestaurantMenuViewModel { RestaurantId = 1, ProductId = 101, ProductName = "Product 1", Price = 10.99 },
                    new RestaurantMenuViewModel { RestaurantId = 1, ProductId = 102, ProductName = "Product 2", Price = 8.99 },
                    new RestaurantMenuViewModel { RestaurantId = 2, ProductId = 101, ProductName = "Product 3", Price = 1.99 },
                    new RestaurantMenuViewModel { RestaurantId = 2, ProductId = 103, ProductName = "Product 4", Price = 12.99 }
                };
            restaurantService.Setup(service => service.GetRestaurantMenu(1))
                                 .Returns(mockMenuItems); 

            // Act
            var result = controller.ShowMenu(1);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        public void Dispose()
        {
            controller.Dispose();
        }
    }
}
