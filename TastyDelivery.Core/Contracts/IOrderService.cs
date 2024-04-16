using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.OrderModels;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Contracts
{
    public interface IOrderService
    {
        public Task<Order> CreateOrder(CheckoutViewModel model);

        public List<OrderDetailsViewModel> GetUserOrders(ApplicationUser user);
    }
}
