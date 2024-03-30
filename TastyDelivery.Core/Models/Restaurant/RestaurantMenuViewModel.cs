namespace TastyDelivery.Core.Models.Restaurant
{
    public class RestaurantMenuViewModel
    {
        public int RestaurantId { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
