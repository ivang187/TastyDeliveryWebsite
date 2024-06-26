﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.DeliveryManModels;
using TastyDelivery.Core.Models.OrderModels;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Services
{
    public class DeliveryManService : IDeliveryManService
    {
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public DeliveryManService(IRepository _repository, UserManager<ApplicationUser> _userManager)
        {
            repository = _repository;
            userManager = _userManager;
        }

        public async Task AssignOrderToWorker(int orderId, string userId)
        {
            var order = FindOrderById(orderId);
            await UpdateOrder(order, userId);
            await repository.SaveChanges();
        }

        private async Task UpdateOrder(Order order, string userId)
        {
            var deliveryMan = await userManager.FindByIdAsync(userId);
            order.Status = DeliveryStatus.OutForDelivery;
            order.DeliveryMan = deliveryMan;
            order.DeliveryManId = deliveryMan.Id;

            repository.Update(order);
        }

        public List<OrderDetailsViewModel> GetPendingDeliveries()
        {
            var orders = repository.AllReadOnly<Order>()
                .Include(o => o.Products)
                .ThenInclude(p => p.Product)
                .Where(o => o.Status == DeliveryStatus.Pending)
                .ToList();

            var orderViewModels = new List<OrderDetailsViewModel>();

            if(orders.Any())
            {
                foreach (var order in orders)
                {
                    var model = CreateOrderViewModel(order);
                    orderViewModels.Add(model);
                }

                return orderViewModels; 
            }

            return null;
        }

        private OrderDetailsViewModel CreateOrderViewModel(Order order)
        {
            var user = repository.AllReadOnly<ApplicationUser>().FirstOrDefault(u => u.Id == order.UserId);
            var restaurant = repository.AllReadOnly<Restaurant>().FirstOrDefault(r => r.Id == order.RestaurantId);

            return new OrderDetailsViewModel
            {
                User = user,
                FullName = user.FirstName + " " + user.LastName,
                OrderId = order.Id,
                Address = order.HomeAddress,
                PhoneNumber = order.PhoneNumber,
                TotalPrice = order.TotalPrice,
                CreatedOrder = order.TimeOrdered,
                ExpectedDelivery = order.TimeOrdered.AddMinutes(40),
                RestaurantName = restaurant.Name,
                Products = order.Products.Select(p => new CartItemViewModel
                {
                    Name = p.Product.Name,
                }).ToList()
            };
        }
        private Order FindOrderById(int orderId)
        {
            return repository.AllReadOnly<Order>().FirstOrDefault(o => o.Id == orderId);
        }

        public AssignedOrdersViewModel CreateAssignedOrderModel(int orderId, List<OrderProducts> products)
        {
            var order = FindOrderById(orderId);
            var user = repository.AllReadOnly<ApplicationUser>().FirstOrDefault(u => u.Id == order.UserId);
            var restaurant = repository.AllReadOnly<Restaurant>().FirstOrDefault(r => r.Id == order.RestaurantId);
            var deliveryMan = repository.AllReadOnly<ApplicationUser>().FirstOrDefault(u => u.Id == order.DeliveryManId);
            var productCartItem = new List<CartItemViewModel>();

            foreach (var product in products)
            {
                productCartItem.Add(new CartItemViewModel
                {
                    Name = product.Product.Name
                });
            }

            return new AssignedOrdersViewModel
            {
                User = user,
                UserFullName = user.FirstName,
                Restaurant = restaurant,
                OrderId = orderId,
                Order = order,
                RestaurantName = restaurant.Name,
                Address = order.HomeAddress,
                DeliveryMan = deliveryMan,
                DeliveryManFullName = deliveryMan.FirstName + " " + deliveryMan.LastName,
                OrderTaken = DateTime.Now,
                PhoneNumber = order.PhoneNumber,
                TotalPrice = order.TotalPrice,
                Products = productCartItem
            };
        }

        public List<AssignedOrdersViewModel> GetAssignedOrders(string userId)
        {
            var currentDeliveryManOrders = repository.AllReadOnly<Order>()
                .Include(o => o.DeliveryMan)
                .Include(o => o.Products)
                    .ThenInclude(p => p.Product)
                .Where(o => o.DeliveryManId == userId)
                .ToList();

            var model = new List<AssignedOrdersViewModel>();

            if (currentDeliveryManOrders.Any())
            {
                foreach (var order in currentDeliveryManOrders)
                {
                    if (order.Status == DeliveryStatus.OutForDelivery)
                    {
                        var item = CreateAssignedOrderModel(order.Id, order.Products.ToList());
                        model.Add(item);
                    }
                }

                return model;
            }
            return null;               
        }

        public void DeliverOrder(int orderId)
        {
            var order = FindOrderById(orderId);

            order.Status = DeliveryStatus.Delivered;
            order.TimeDelivered = DateTime.Now;

            repository.Update(order);
            repository.SaveChanges();
        }
    }
}
