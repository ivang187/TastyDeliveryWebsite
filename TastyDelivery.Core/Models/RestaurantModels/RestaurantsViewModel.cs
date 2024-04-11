namespace TastyDelivery.Models.RestaurantModels 
{
    public class RestaurantsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string WorkingHours { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
    }
}
