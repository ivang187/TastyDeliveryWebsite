using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TastyDelivery.Infrastructure.Utilities.Constants
{
    public class UsersConstants
    {
        public const int UserFirstNameMinLength = 1;
        public const int UserFirstNameMaxLength = 50;

        public const int UserLastNameMinLength = 1;
        public const int UserLastNameMaxLength = 50;

        public const int AddressNameMaxLength = 100;
        public const int AddressNameMinLength = 10;
    }
}
