using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Models.OrderModels
{
    public class OrderDetailsViewModel
    {
        public ApplicationUser User { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int OrderId { get; set; }

        public Restaurant Restaurant { get; set; }

        public string RestaurantName { get; set; }
        public List<CartItemViewModel> Products { get; set; }

        public double TotalPrice { get; set; }

        public DeliveryStatus Status { get; set; }

        public DateTime CreatedOrder { get; set; }

        public DateTime ExpectedDelivery { get; set; }
    }
}
