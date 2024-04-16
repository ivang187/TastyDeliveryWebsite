using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.OrderModels;

namespace TastyDelivery.Core.Models.DeliveryManModels
{
    public class DeliveryManHomePageModel
    {
        public List<AssignedOrdersViewModel> AssignedOrders { get; } = new List<AssignedOrdersViewModel>();
    }
}
