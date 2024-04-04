using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TastyDelivery.Areas.Admin;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Models;

namespace TastyDelivery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestaurantService _restaurantService;

        public HomeController(ILogger<HomeController> logger, IRestaurantService restaurantService)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        public IActionResult Index()
        {

            if (User.IsInRole(AdminConstants.AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new {area = "Admin"});
            }
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Restaurants", "Restaurant");
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
