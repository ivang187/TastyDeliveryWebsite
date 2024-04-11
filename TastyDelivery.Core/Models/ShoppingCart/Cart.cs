using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Core.Models.ShoppingCart
{
    public class Cart
    {
        public string RestaurantName { get; set; }
        public List<CartItemViewModel> Products { get; set; } = new List<CartItemViewModel>();
    }
}
