﻿using System;
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
        public const int UserFirstNameMaxLength = 30;

        public const int UserLastNameMinLength = 1;
        public const int UserLastNameMaxLength = 30;

        public const int CityNameMaxLength = 30;
        public const int CityNameMinLength = 1;

        public const int AddressMaxLength = 50;
        public const int AddressMinLength = 1;
    }
}
