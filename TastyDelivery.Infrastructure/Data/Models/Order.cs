using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using System.Runtime.Serialization;

namespace TastyDelivery.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public string HomeAddress { get; set; } = string.Empty;

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public DeliveryStatus Status { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; }

        [Required]
        public string DeliveryManId { get; set; }

        [ForeignKey(nameof(DeliveryManId))]
        public ApplicationUser DeliveryMan { get; set; }

        public ICollection<OrderProducts> Products { get; set; } = new List<OrderProducts>();
    }
}
