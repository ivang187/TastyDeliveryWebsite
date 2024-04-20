﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.AdminModels;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TastyDelivery.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;
        
        public AdminService(IRepository _repository, UserManager<ApplicationUser> _userManager) 
        {
            repository = _repository;
            userManager = _userManager;
        }
        public Restaurant CreateRestaurant(string name, string workingHours, string location)
        {
            var model = new Restaurant
            {
                Name = name,
                WorkingHours = workingHours,
                Location = location
            };

            return model;
        }

        public ProductsRestaurants CreateProduct(int restaurantId, string name, string description, ProductCategory category, double price)
        {
            var product = new Product { Name = name, Description = description, Category = category };

            var model = new ProductsRestaurants
            {
                RestaurantId = restaurantId,
                Product = product,
                Price = price
            };

            return model;
        }

        public async Task CreateDriver(AppointDriverModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                user = CreateNewDriver(model);
                repository.AddNew(user);
            }
            else
            {
                await UpdateUserRole(user);
                user.Role = UserRole.DeliveryMan;
                user.HomeAddress = null;
                repository.Update(user);
            }

            await repository.SaveChanges();
        }

        private async Task UpdateUserRole(ApplicationUser user)
        {
            var existingRoles = await userManager.GetRolesAsync(user);
            var removeFromRoles = await userManager.RemoveFromRolesAsync(user, existingRoles);

            var addToRoleResult = await userManager.AddToRoleAsync(user, UserRole.DeliveryMan.ToString());
        }

        private ApplicationUser CreateNewDriver(AppointDriverModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Role = UserRole.DeliveryMan,
                UserName = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                NormalizedUserName = model.Email.ToUpper()
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

            return user;
        }

        public List<CompletedDeliveriesAdminViewModel> GetCompletedDeliveries()
        {
            var orders = repository.AllReadOnly<Order>().Where(o => o.Status == DeliveryStatus.Delivered).ToList();

            var model = new List<CompletedDeliveriesAdminViewModel>();

            if(orders.Any())
            {
                foreach (var order in orders)
                {
                    var item = CreateCompleteDeliveryViewModel(order);
                    model.Add(item);
                }

                return model;
            }

            return null;
            
        }

        private CompletedDeliveriesAdminViewModel CreateCompleteDeliveryViewModel(Order order)
        {
            var deliveryMan = repository.AllReadOnly<ApplicationUser>().FirstOrDefault(u => u.Id == order.DeliveryManId);
            var restaurantName = repository.AllReadOnly<Restaurant>().Where(r => r.Id == order.RestaurantId).Select(r => r.Name).FirstOrDefault();
            var user = repository.AllReadOnly<ApplicationUser>().FirstOrDefault(u => u.Id == order.UserId);

            return new CompletedDeliveriesAdminViewModel
            {
                DeliveryMan = deliveryMan,
                DeliveryManName = deliveryMan.FirstName + " " + deliveryMan.LastName,
                TimeDelivered = order.TimeDelivered,
                TimeOrdered = order.TimeOrdered,
                Order = order,
                OrderId = order.Id,
                RestaurantName = restaurantName,
                User = user,
                UserName = user.FirstName + " " + user.LastName,
            };
        }
    }
}
