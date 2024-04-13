using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderService(IRepository _repository, UserManager<ApplicationUser> _userManager)
        {
            repository = _repository;
            userManager = _userManager;
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

            AddToOrderCount(order);

            return order;
        }

        private void AddToOrderCount(Order order)
        {        
            var restaurant = repository.AllReadOnly<Restaurant>().Where(r => r.Id == order.RestaurantId).FirstOrDefault();
            var user = order.User;

            user.Orders.Add(order);
            restaurant.Orders.Add(order);

            user.OrderCount += 1;
            restaurant.OrderCount += 1;

            repository.Update(user);
            repository.Update(restaurant);
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
                RestaurantName = repository.AllReadOnly<Restaurant>().Where(r => r.Id == order.RestaurantId).Select(r => r.Name).FirstOrDefault(),
                Restaurant = repository.AllReadOnly<Restaurant>().FirstOrDefault(r => r.Id == order.RestaurantId),
            };

            return model;
        }
    }
}
