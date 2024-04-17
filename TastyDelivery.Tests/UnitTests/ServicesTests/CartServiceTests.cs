using Microsoft.AspNetCore.Cors.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Services;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class CartServiceTests : UnitTestsBase
    {
        private Mock<IRepository> repository;
        private ShoppingCartService shoppingCartService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository>();
            shoppingCartService = new ShoppingCartService(repository.Object);
        }

        [Test]
        public void FindItemToAdd_ReturnsCartItemViewModel()
        {
            int productId = 1;
            double price = 10.99;
            int quantity = 2;

            var mockProduct = new ProductsRestaurants
            {
                ProductId = productId,
                Product = new Product { Name = "Test Product" }
            };

            repository.Setup(repo => repo.AllReadOnly<ProductsRestaurants>())
            .Returns(new List<ProductsRestaurants> { mockProduct }.AsQueryable());


            var result = shoppingCartService.FindItemToAdd(productId, price, quantity);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(productId));
            Assert.That(result.Name, Is.EqualTo("Test Product"));
            Assert.That(result.Price, Is.EqualTo(price));
            Assert.That(result.Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void FindItemToRemove_ReturnsCartItemViewModel()
        {
            // Arrange
            int productId = 1;
            var mockProduct = new Product { Id = productId };

            repository.Setup(repo => repo.AllReadOnly<Product>())
                          .Returns(new List<Product> { mockProduct }.AsQueryable());


            // Act
            var result = shoppingCartService.FindItemToRemove(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(productId));
        }
    }
}
