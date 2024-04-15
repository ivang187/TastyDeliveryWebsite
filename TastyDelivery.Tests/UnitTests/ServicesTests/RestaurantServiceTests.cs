using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
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

    }
}
