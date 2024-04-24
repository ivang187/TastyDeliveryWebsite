using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Core.Models.OrderModels
{
    public class CheckoutViewModel
    {
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Please fill First Name field!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please fill Last Name field!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please fill Address field!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please fill Phone field")]
        public string Phone { get; set; }

        public Restaurant Restaurant { get; set; }
        public string RestaurantName { get; set; }

        public List<CartItemViewModel> Products { get; set; } = new List<CartItemViewModel>();   
    }
}
