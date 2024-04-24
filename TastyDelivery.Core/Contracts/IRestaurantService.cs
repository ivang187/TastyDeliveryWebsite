using TastyDelivery.Core.Models.AdminModels;
using TastyDelivery.Core.Models.RestaurantModels;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Models;
using TastyDelivery.Models.RestaurantModels;

namespace TastyDelivery.Core.Contracts
{
    public interface IRestaurantService
    {
        public string GetRestaurantName(int id);
        public IEnumerable<RestaurantsViewModel> GetAllRestaurants();

        public IEnumerable<RestaurantMenuViewModel> GetRestaurantMenu(int id);

        public List<string> GetDistinctTypes();

        public List<RestaurantsViewModel> GetRestaurantsByType(string type);

        public void Delete(Restaurant restaurant);

        public Restaurant GetRestaurantById(int id);

        public bool CheckForPendingOrders(int restaurantId);

        public bool CheckIfRestaurantExists(string name);

        public void Update(AddRestaurantFormViewModel model);
    }
}
