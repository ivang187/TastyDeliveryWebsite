using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Claims;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.OrderModels;
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

        public OrderService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<Order> CreateOrder(CheckoutViewModel model)
        {
            var order = new Order
            {
                User = model.User,
                UserId = model.User.Id,
                HomeAddress = model.Address,
                PhoneNumber = model.Phone,
                TotalPrice = model.Products.Sum(p => p.Price * p.Quantity),
                TimeOrdered = DateTime.Now,
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

            await SaveChangesToDb(order);

            return order;
        }

        public List<Order> GetRestaurantsOrders(int id)
        {
            return repository.AllReadOnly<Order>().Where(r => r.RestaurantId == id).ToList();  
        }

        public List<OrderDetailsViewModel> GetUserOrders(ApplicationUser user)
        {
            var orders = new List<OrderDetailsViewModel>();
            var userOrders = repository.AllReadOnly<Order>()
                .Include(r => r.User)   
                .Include(r => r.Products)
                    .ThenInclude(r => r.Product)
                    .Where(r => r.UserId == user.Id && r.Status != DeliveryStatus.Delivered)
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
            return new OrderDetailsViewModel
            {
                OrderId = order.Id,
                FullName = user.FirstName + ' ' + user.LastName,
                User = user,
                Address = order.HomeAddress,
                PhoneNumber = order.PhoneNumber,
                Products = order.Products.Select(p => new CartItemViewModel
                {
                    Id = p.ProductId,
                    Name = p.Product.Name
                }).ToList(),
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CreatedOrder = order.TimeOrdered,
                ExpectedDelivery = order.TimeOrdered.AddMinutes(40),
                RestaurantName = repository.AllReadOnly<Restaurant>().Where(r => r.Id == order.RestaurantId).Select(r => r.Name).FirstOrDefault(),
                Restaurant = repository.AllReadOnly<Restaurant>().FirstOrDefault(r => r.Id == order.RestaurantId),
            };
        }

        private async Task SaveChangesToDb(Order order)
        {
            repository.AddNew(order);
            await repository.SaveChanges();
        }
    }
}
