using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Core.Contracts
{
    public interface IShoppingCartService
    {
        public Task<CartItemViewModel> FindItemToAdd(int id, double price, int quantity);

        public Task<CartItemViewModel> FindItemToRemove(int id);
    }
}
