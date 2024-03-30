using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.Restaurant;
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

        public async Task<IEnumerable<RestaurantsViewModel>> GetAllRestaurants()
        {
            return await repository.AllReadOnly<Restaurant>()
                .Select(r => new RestaurantsViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Location = r.Location,
                    WorkingHours = r.WorkingHours
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RestaurantMenuViewModel>> GetRestaurantMenu(int id)
        {
            return await repository.AllReadOnly<ProductsRestaurants>()
                .Where(r => r.RestaurantId == id)
                .Select(pr => new RestaurantMenuViewModel
                {
                    Id = pr.ProductId,
                    Name = pr.Product.Name,
                    Description = pr.Product.Description,
                    Price = pr.Price
                })
                .ToListAsync();
        }
    }
}
