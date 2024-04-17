using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Models.AdminModels
{
    public class CompletedDeliveriesAdminViewModel
    {
        public string DeliveryManName { get; set; }

        public ApplicationUser DeliveryMan { get; set; }

        public string UserName { get; set; }
        public ApplicationUser User { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public DateTime TimeOrdered { get; set; }

        public DateTime TimeDelivered { get; set; }

        public string RestaurantName { get; set; }
    }
}
