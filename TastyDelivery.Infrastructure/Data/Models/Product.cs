using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Utilities.Constants;

namespace TastyDelivery.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AppConstants.ProductNameMaxLength)]
        [MinLength(AppConstants.ProductNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(AppConstants.ProductDescriptionMaxLength)]
        [MinLength(AppConstants.ProductDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public ProductCategory Category { get; set; }

        public ICollection<OrderProducts> Products { get; set; } = new List<OrderProducts>();
    }
}
