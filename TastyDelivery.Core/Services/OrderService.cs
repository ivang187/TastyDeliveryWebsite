using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public OrderService(IRepository _repository)
        {
            repository = _repository;
        }

        public Order CreateOrder(OrderDetailsViewModel model)
        {
            var order = new Order
            {
                User = model.User,
                UserId = model.User.Id,
                TotalPrice = model.TotalPrice,
                Status = model.Status,
                RestaurantId = repository.AllReadOnly<Restaurant>().Where(r => r.Id == model.Restaurant.Id).Select(r => r.Id).FirstOrDefault(),
                Products = model.Products
                    .Where(item => item != null && item.Id != 0)
                    .Select(item => new OrderProducts
                    {
                        ProductId = item.Id
                    })
                    .ToList()
            };

            return order;
        }

        public OrderDetailsViewModel CreateOrderViewModel(CheckoutViewModel model)
        {
            var order = new OrderDetailsViewModel
            {
                FullName = model.FirstName + " " + model.LastName,
                User = model.User,
                Address = model.Address,
                PhoneNumber = model.Phone,
                Products = model.Products,
                TotalPrice = model.Products.Sum(item => item.Price * item.Quantity),
                Status = DeliveryStatus.Pending,
                RestaurantName = model.RestaurantName,
                Restaurant = model.Restaurant
            };

            return order;
        }
    }
}
