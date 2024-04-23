using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyDelivery.Infrastructure.Utilities.Constants
{
    public class AppConstants
    {
        public const int ProductNameMaxLength = 50;
        public const int ProductNameMinLength = 3;

        public const int ProductDescriptionMaxLength = 100;
        public const int ProductDescriptionMinLength = 5;

        public const int RestaurantNameMaxLength = 50;
        public const int RestaurantNameMinLength = 3;

        public const int RestaurantWorkingHoursMaxLength = 14;

        public const int RestaurantLocationMaxLength = 80;
        public const int RestaurantLocationMinLength = 5;

        public const int RestaurantTypeMinLength = 3;
        public const int RestaurantTypeMaxLength = 30;
    }
}
