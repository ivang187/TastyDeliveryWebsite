using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Models.AdminModels
{
    public class PendingDeliveriesViewModel
    {
        public int OrderId { get; set; }

        public Order Order { get; set; }    

        public ApplicationUser Customer {  get; set; }

        public string CustomerName { get; set; }

        public DateTime TimeOrdered { get; set; }

        public Restaurant Restaurant { get; set; }

        public string RestaurantName { get; set; }
    }
}
