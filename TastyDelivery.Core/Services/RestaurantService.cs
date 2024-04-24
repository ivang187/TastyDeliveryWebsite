using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.RestaurantModels;
using TastyDelivery.Models.RestaurantModels;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TastyDelivery.Core.Models.AdminModels;
using TastyDelivery.Infrastructure.Data.Models.Enums;

namespace TastyDelivery.Core.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository repository;

        public RestaurantService(IRepository _repository)
        {
            repository = _repository;
        }

        public string GetRestaurantName(int id)
        {
            return repository.AllReadOnly<Restaurant>()
                .Where(r => r.Id == id)
                .Select(x => x.Name).FirstOrDefault();
        }

        public IEnumerable<RestaurantsViewModel> GetAllRestaurants()
        {
            return repository.AllReadOnly<Restaurant>()
                .Select(r => new RestaurantsViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Type = r.Type,
                    Location = r.Location,
                    WorkingHours = r.WorkingHours
                })
                .ToList();
        }

        public List<string> GetDistinctTypes()
        {
            return repository.AllReadOnly<Restaurant>().Select(r => r.Type).Distinct().ToList();
        }

        public IEnumerable<RestaurantMenuViewModel> GetRestaurantMenu(int id)
        {
            return repository.AllReadOnly<ProductsRestaurants>()
                .Where(r => r.RestaurantId == id)
                .Select(pr => new RestaurantMenuViewModel
                {
                    RestaurantName = pr.Restaurant.Name,
                    RestaurantId = pr.RestaurantId,
                    ProductId = pr.ProductId,
                    ProductName = pr.Product.Name,
                    Description = pr.Product.Description,
                    Price = pr.Price,
                    Category = pr.Product.Category
                })
                .ToList();
        }

        public List<RestaurantsViewModel> GetRestaurantsByType(string type)
        {
            return repository.AllReadOnly<Restaurant>()
                .Where(r => r.Type == type)
                .Select(r => new RestaurantsViewModel
                {
                    Id = r.Id,
                    Location = r.Location,
                    Name = r.Name,
                    Type = r.Type,
                    WorkingHours = r.WorkingHours
                })
                .ToList();
        }

        public void Delete(Restaurant restaurant)
        {
            repository.Delete(restaurant);
            repository.SaveChanges();
        }

        public async Task<Restaurant> GetRestaurantById(int id)
        {
            var restaurant = await repository.AllReadOnly<Restaurant>().FirstOrDefaultAsync(r => r.Id == id);

            return restaurant;
        }

        public bool CheckForPendingOrders(int restaurantId)
        {
            var orders = repository.AllReadOnly<Order>().Where(o => o.RestaurantId == restaurantId && o.Status == DeliveryStatus.Pending).ToList();

            if(orders.Any())
            {
                return true;
            }

            return false;
        }

        public bool CheckIfRestaurantExists(string name)
        {
            var restaurant = repository.AllReadOnly<Restaurant>().FirstOrDefault(r => r.Name == name);

            if(restaurant == null)
            {
                return false;
            }

            return true;
        }

        public void Update(AddRestaurantFormViewModel model)
        {
            var restaurant = repository.AllReadOnly<Restaurant>().FirstOrDefault(r => r.Name == model.Name);

            restaurant.WorkingHours = model.WorkingHours;
            restaurant.Location = model.Location;
            restaurant.Type = model.Type;

            repository.Update(restaurant);
            repository.SaveChanges();
        }
    }
}
