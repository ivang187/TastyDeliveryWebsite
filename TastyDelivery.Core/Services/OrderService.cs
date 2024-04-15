using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Claims;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.Order;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Services
{
    public class OrderService : IOrderService
    {
        private IRepository repository;

        public OrderService(IRepository repository)
        {
            this.repository = repository;
        }

        public Order CreateOrder(CheckoutViewModel model)
        {
            var order = new Order
            {
                User = model.User,
                UserId = model.User.Id,
                HomeAddress = model.Address,
                TotalPrice = model.Products.Sum(p => p.Price * p.Quantity),
                Status = DeliveryStatus.Pending,
                RestaurantId = repository.AllReadOnly<Restaurant>().Where(r => r.Id == model.Restaurant.Id).Select(r => r.Id).FirstOrDefault(),
                Products = model.Products
                    .Where(item => item != null && item.Id != 0)
                    .Select(item => new OrderProducts
                    {
                        ProductId = item.Id
                    })
                    .ToList()
            };

            repository.AddNew(order);
            repository.SaveChanges();

            return order;
        }

        public List<OrderDetailsViewModel> GetUserOrders(ApplicationUser user)
        {
            var orders = new List<OrderDetailsViewModel>();
            var userOrders = repository.AllReadOnly<ApplicationUser>()
                    .Include(o => o.Orders)
                        .ThenInclude(o => o.Products)
                            .ThenInclude(op => op.Product) 
                    .Where(o => o.Id == user.Id)
                    .SelectMany(o => o.Orders)
                    .ToList();


            if (userOrders.Any())
            {
                foreach (var order in userOrders)
                {
                    var model = CreateOrderViewModel(order, user);
                    orders.Add(model);
                }

                return orders;
            }

            return null;
        }

        private OrderDetailsViewModel CreateOrderViewModel(Order order, ApplicationUser user)
        {
            var dateNow = DateTime.Now;

            var model = new OrderDetailsViewModel
            {
                OrderId = order.Id,
                FullName = user.FirstName + ' ' + user.LastName,
                User = user,
                Address = order.HomeAddress,
                PhoneNumber = user.PhoneNumber,
                Products = order.Products.Select(p => new CartItemViewModel
                {
                    Id = p.ProductId,
                    Name = p.Product.Name
                }).ToList(),
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CreatedOrder = dateNow,
                ExpectedDelivery = dateNow.AddMinutes(40),
                RestaurantName = repository.AllReadOnly<Restaurant>().Where(r => r.Id == order.RestaurantId).Select(r => r.Name).FirstOrDefault(),
                Restaurant = repository.AllReadOnly<Restaurant>().FirstOrDefault(r => r.Id == order.RestaurantId),
            };

            return model;
        }
    }
}
