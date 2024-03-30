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
        public async Task<IActionResult> Restaurants()
        {
            var model = await _restaurantService.GetAllRestaurants();

            return View(model);
        }

        public async Task<IActionResult> ShowMenu(int id)
        {
            var model = await _restaurantService.GetRestaurantMenu(id);

            if(model == null)
            {
                return BadRequest();
            }

            return View(model);
        }
    }
}
