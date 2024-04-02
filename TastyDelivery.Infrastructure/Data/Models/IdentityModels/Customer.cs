using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyDelivery.Infrastructure.Data.Models.IdentityModels
{
    public class Customer : ApplicationUser
    {
        public string Address { get; set; } = string.Empty;

        public override string PhoneNumber { get; set; } = string.Empty;
    }
}
