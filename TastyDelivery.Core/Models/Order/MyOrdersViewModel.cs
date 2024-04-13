using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyDelivery.Core.Models.Order
{
    public class MyOrdersViewModel
    {
        public List<OrderDetailsViewModel> Orders { get; set; } = new List<OrderDetailsViewModel>();
    }
}
