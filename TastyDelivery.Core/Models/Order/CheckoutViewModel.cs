using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Models.Order
{
    public class CheckoutViewModel
    {
        public ApplicationUser User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Restaurant { get; set; }

        public ICollection<CartItemViewModel> Products { get; set; } = new List<CartItemViewModel>();   
    }
}
