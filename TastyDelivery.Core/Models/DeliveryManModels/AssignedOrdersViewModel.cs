using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Core.Models.DeliveryManModels
{
    public class AssignedOrdersViewModel
    {
        public ApplicationUser User { get; set; }
        public string UserFullName { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public Restaurant Restaurant { get; set; }

        public string RestaurantName { get; set; }
        public List<CartItemViewModel> Products { get; set; }

        public double TotalPrice { get; set; }

        public DateTime OrderTaken { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public ApplicationUser DeliveryMan { get; set; }

        public string DeliveryManFullName { get; set; }
    }
}
