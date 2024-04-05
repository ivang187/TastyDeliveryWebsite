using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        [Required]
        public ICollection<OrderProducts> Products { get; set; } = new List<OrderProducts>();

    }
}
