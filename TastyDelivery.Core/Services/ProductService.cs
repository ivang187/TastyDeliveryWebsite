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
    public class ProductService : IProductService
    {
        private readonly IRepository repository;

        public ProductService(IRepository _repository)
        {
            repository = _repository;
        }

        public ProductsRestaurants Create(int restaurantId, string name, string description, ProductCategory category, double price)
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

        public void AddToDb(ProductsRestaurants product)
        {
            repository.AddNew(product);
            repository.SaveChanges();
        }
    }
}
