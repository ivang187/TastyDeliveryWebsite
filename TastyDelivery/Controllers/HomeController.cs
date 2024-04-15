using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TastyDelivery.Areas.Admin;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Extensions;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using TastyDelivery.Infrastructure.Utilities.Constants;
using TastyDelivery.Models;

namespace TastyDelivery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestaurantService _restaurantService;

        public HomeController(ILogger<HomeController> logger,
            IRestaurantService restaurantService)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(AdminConstants.AdminRoleName))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                if(User.IsInRole(UsersConstants.DeliveryManRoleName))
                {
                    return RedirectToAction("Index", "DeliveryMan");
                }
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
