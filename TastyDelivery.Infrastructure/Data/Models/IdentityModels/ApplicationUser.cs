using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
