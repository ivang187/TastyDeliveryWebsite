using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Infrastructure.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public int TotalPrice { get; set; }
    }
}
