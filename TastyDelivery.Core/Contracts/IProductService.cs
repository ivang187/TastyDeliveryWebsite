using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;

namespace TastyDelivery.Core.Contracts
{
    public interface IProductService
    {
        public ProductsRestaurants Create(int restaurantId, string name, string description, ProductCategory category, double price);

        public void AddToDb(ProductsRestaurants product);
    }
}
