﻿using System;
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
        public void CreateRestaurant(AddRestaurantFormViewModel model);

        public ProductsRestaurants CreateProduct(int restaurantId, int productId, string name, string description, ProductCategory category, double price);

        public Task CreateDriver(AppointDriverModel model);

        public List<CompletedDeliveriesAdminViewModel> GetCompletedDeliveries();

        public ProductsRestaurants GetProductById(int id);

        public void DeleteProduct(ProductsRestaurants product);

        public List<PendingDeliveriesViewModel> GetPendingDeliveries();
    }
}
