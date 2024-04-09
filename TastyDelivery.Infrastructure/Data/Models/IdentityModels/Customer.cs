using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models.Enums;

namespace TastyDelivery.Infrastructure.Data.Models.IdentityModels
{
    public class Customer : ApplicationUser
    {
        public override UserRole Role
        {
            get => base.Role;
            set
            {
                if (value != UserRole.Customer)
                {
                    throw new InvalidOperationException("Customer role must be UserRole.Customer.");
                }
                base.Role = value;
            }
        }

        public string HomeAddress { get; set; } = string.Empty;

        public override string PhoneNumber { get; set; } = string.Empty;
    }
}
