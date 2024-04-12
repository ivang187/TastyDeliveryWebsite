using System;
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

namespace TastyDelivery.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository repository;
        
        public AdminService(IRepository _repository) 
        {
            repository = _repository;
        }
        public async Task<Restaurant> CreateRestaurant(string name, string workingHours, string location)
        {
            var model = new Restaurant
            {
                Name = name,
                WorkingHours = workingHours,
                Location = location
            };

            return await Task.FromResult(model);
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

        public void CreateDriver(AppointDriverModel model)
        {
            var user = FindUserByEmail(model.Email);

            if (user == null)
            {
                user = CreateNewDriver(model);
                repository.AddNew(user);
            }
            else
            {
                user.Role = UserRole.DeliveryMan;
                user.HomeAddress = null;
                repository.Update(user);
            }

            repository.SaveChanges();
        }

        private ApplicationUser FindUserByEmail(string email)
        {
            var userEmail = repository.AllReadOnly<ApplicationUser>().Where(u => u.Email == email).Select(u => u.Email).FirstOrDefault();

            if(userEmail == null)
            {
                return null;
            }

            return repository.AllReadOnly<ApplicationUser>().FirstOrDefault(u => u.Email == userEmail);
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
    }
}
