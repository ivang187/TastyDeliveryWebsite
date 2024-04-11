using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.Order;

namespace TastyDelivery.Core.Contracts
{
    public interface IOrderService
    {
        public void CreateOrder();
    }
}
