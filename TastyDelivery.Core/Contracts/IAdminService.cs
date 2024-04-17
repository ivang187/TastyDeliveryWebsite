using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.AdminModels;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Contracts
{
    public interface IAdminService
    {
        public Restaurant CreateRestaurant(string name, string workingHours, string location);

        public ProductsRestaurants CreateProduct(int restaurantId, string name, string description, ProductCategory category, double price);

        public Task CreateDriver(AppointDriverModel model);

        public Task<List<CompletedDeliveriesAdminViewModel>> GetCompletedDeliveries();
    }
}
