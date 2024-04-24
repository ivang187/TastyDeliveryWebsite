using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Models.RestaurantModels;

namespace TastyDelivery.Core.Models.RestaurantModels
{
    public class RestaurantModel
    {
        [Required]
        public List<RestaurantsViewModel> Restaurants { get; set; } = new List<RestaurantsViewModel>();

        [Required(ErrorMessage = "Type in atleast one menu item to add!")]
        public string MenuItems { get; set; }

        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public int ProductId { get; set; }
    }
}
