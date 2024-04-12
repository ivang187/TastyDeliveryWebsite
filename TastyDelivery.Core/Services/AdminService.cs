using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;

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

        public void AddToDb(Restaurant restaurant)
        {
            repository.AddNew(restaurant);
            repository.SaveChanges();
        }


    }
}
