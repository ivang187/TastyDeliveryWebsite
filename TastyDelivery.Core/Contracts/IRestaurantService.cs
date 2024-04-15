using TastyDelivery.Core.Models.RestaurantModels;
using TastyDelivery.Models;
using TastyDelivery.Models.RestaurantModels;

namespace TastyDelivery.Core.Contracts
{
    public interface IRestaurantService
    {
        public string GetRestaurantName(int id);
        public IEnumerable<RestaurantsViewModel> GetAllRestaurants();

        public IEnumerable<RestaurantMenuViewModel> GetRestaurantMenu(int id);
    }
}
