using TastyDelivery.Infrastructure.Data.Models.Enums;

namespace TastyDelivery.Core.Models.RestaurantModels
{
    public class RestaurantMenuViewModel
    {
        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Quantity { get; set; }

        public ProductCategory Category { get; set; }
    }
}
