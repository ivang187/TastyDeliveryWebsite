using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyDelivery.Core.Contracts;

namespace TastyDelivery.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService) 
        {
            _restaurantService = restaurantService;
        }
        public IActionResult Restaurants(string restaurantType)
        {
            if(restaurantType == null)
            {
                var allRestaurants = _restaurantService.GetAllRestaurants();
                return View(allRestaurants);
            }

            var model = _restaurantService.GetRestaurantsByType(restaurantType);

            return View(model);
        }

        public IActionResult ShowMenu(int id)
        {
            var model = _restaurantService.GetRestaurantMenu(id);
            var restaurantName = _restaurantService.GetRestaurantName(id);


            if(model == null || !model.Any())
            {
                int statusCode = 404;
                return RedirectToAction("Error", "Home", new { statusCode = statusCode });
            }

            ViewBag.Title = $"{restaurantName} \nMenu";

            return View(model);
        }
    }
}
