using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Utilities.Constants;

namespace TastyDelivery.Infrastructure.Data.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(UsersConstants.UserFirstNameMaxLength)]
        [MinLength(UsersConstants.UserFirstNameMinLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(UsersConstants.UserLastNameMaxLength)]
        [MinLength(UsersConstants.UserLastNameMinLength)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(UsersConstants.AddressNameMaxLength)]
        [MinLength(UsersConstants.AddressNameMinLength)]
        public string HomeAddress { get; set; } = string.Empty;

        public virtual UserRole Role { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
