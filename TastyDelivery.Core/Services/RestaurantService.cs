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
                    Location = r.Location,
                    WorkingHours = r.WorkingHours
                })
                .ToList();
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
    }
}
