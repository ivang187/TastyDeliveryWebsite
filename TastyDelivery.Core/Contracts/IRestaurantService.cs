using TastyDelivery.Core.Models.RestaurantModels;
using TastyDelivery.Models;
using TastyDelivery.Models.RestaurantModels;

namespace TastyDelivery.Core.Contracts
{
    public interface IRestaurantService
    {
        public Task<string> GetRestaurantName(int id);
        public Task<IEnumerable<RestaurantsViewModel>> GetAllRestaurants();

        public Task<IEnumerable<RestaurantMenuViewModel>> GetRestaurantMenu(int id);
    }
}
