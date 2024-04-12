using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.Order;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Core.Contracts
{
    public interface IOrderService
    {
        public Order CreateOrder(OrderDetailsViewModel model);

        public OrderDetailsViewModel CreateOrderViewModel(CheckoutViewModel model);
    }
}
