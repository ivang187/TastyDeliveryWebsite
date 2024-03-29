using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TastyDelivery.Infrastructure.Utilities.Constants;

namespace TastyDelivery.Infrastructure.Data.Models
{
    public class Restaurant
    {
        [Key]
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

        public ICollection<ProductsRestaurants> Products { get; set; } = new List<ProductsRestaurants>();
    }
}