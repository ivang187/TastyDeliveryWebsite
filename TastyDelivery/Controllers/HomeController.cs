using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }
            if (statusCode == 401)
            {
                return View("Error401");
            }
            if(statusCode == 404)
            {
                return View("Error404");
            }

            return View();
        }
    }
}
