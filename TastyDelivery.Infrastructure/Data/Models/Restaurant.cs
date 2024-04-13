using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TastyDelivery.Infrastructure.Utilities.Constants;

namespace TastyDelivery.Infrastructure.Data.Models
{
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(AppConstants.RestaurantNameMaxLength)]
        [MinLength(AppConstants.RestaurantNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(AppConstants.RestaurantWorkingHoursMaxLength)]
        public string WorkingHours { get; set; } = string.Empty;

        [Required]
        [MaxLength(AppConstants.RestaurantLocationMaxLength)]
        [MinLength(AppConstants.RestaurantLocationMinLength)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public int OrderCount { get; set; }

        public ICollection<ProductsRestaurants> Products { get; set; } = new List<ProductsRestaurants>();

        public ICollection<Order> Orders { get; set; } = new List<Order>(); 
    }
}