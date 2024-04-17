using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Controllers;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Services;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyDelivery.Core.Models.OrderModels;
using TastyDelivery.Models.RestaurantModels;

namespace TastyDelivery.Tests.UnitTests.ControllerTests
{
    [TestFixture]
    public class OrderControllerTests : UnitTestsBase, IDisposable
    {
        private OrderController orderController;
        private Mock<IRepository> repository;
        private Mock<IOrderService> orderService;
        private Mock<UserManager<ApplicationUser>> userManager;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository>();
            orderService = new Mock<IOrderService>();
            userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            orderController = new OrderController(userManager.Object, repository.Object, orderService.Object);
        }

        [Test]
        public async Task OrderDetails_ReturnsCorrectInformation()
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
                UserName = "ivan@gmail.com",
                Orders = new List<Order>()
            };

            var order = new Order
            {
                Id = 1,
                UserId = mockUser.Id,
                User = mockUser,
                HomeAddress = "fsdfsgdgd",
                PhoneNumber = "FSDGSDGADSF",
                TotalPrice = 12.21,
                Status = DeliveryStatus.OutForDelivery,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "Sote" }
            };

            var order2 = new Order
            {
                Id = 2,
                UserId = mockUser.Id,
                User = mockUser,
                HomeAddress = "fsdfsgdgd",
                PhoneNumber = "FSDGSDGADSF",
                TotalPrice = 12.21,
                Status = DeliveryStatus.Pending,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "Sote" }
            };

            var order3 = new Order
            {
                Id = 3,
                UserId = mockUser.Id,
                User = mockUser,
                HomeAddress = "fsdfsgdgd",
                PhoneNumber = "FSDGSDGADSF",
                TotalPrice = 12.21,
                Status = DeliveryStatus.Delivered,
                RestaurantId = 1,
                Restaurant = new Restaurant { Id = 1, Name = "Sote" }
            };

            var viewModels = new List<OrderDetailsViewModel>
            {
                new OrderDetailsViewModel
                {
                    OrderId = 1
                },
                new OrderDetailsViewModel 
                {
                    OrderId = 2
                }
            };

            var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "1");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.User)
                           .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { userIdClaim }, "mockAuthenticationType")));

            orderController.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            userManager.Setup(u => u.FindByIdAsync(mockUser.Id)).ReturnsAsync(mockUser);
            repository.Setup(r => r.AllReadOnly<Order>()).Returns(new List<Order> { order, order2, order3 }.AsQueryable());
            orderService.Setup(o => o.GetUserOrders(mockUser)).Returns(viewModels);

            var result = await orderController.OrderDetails() as ViewResult;

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(result.Model, Is.InstanceOf<List<OrderDetailsViewModel>>());

            var model = result.Model as List<OrderDetailsViewModel>;
            Assert.That(model, Contains.Item(viewModels[0]));
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            orderController.Dispose();
        }
    }
}
