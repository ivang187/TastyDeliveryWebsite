using Microsoft.AspNetCore.Mvc;
using Moq;
using TastyDelivery.Controllers;
using TastyDelivery.Core.Contracts;
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
                    WorkingHours = "8:00-14:00"
                }
            };

            restaurantService.Setup(s => s.GetAllRestaurants()).Returns(expectedModel);

            var result = controller.Restaurants() as ViewResult;

            Assert.IsNotNull(result);
            Assert.That(result.Model, Is.InstanceOf<IEnumerable<RestaurantsViewModel>>());

            var model = result.Model as IEnumerable<RestaurantsViewModel>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model, Contains.Item(expectedModel[0]));
        }

        public void Dispose()
        {
            controller.Dispose();
        }
    }
}
