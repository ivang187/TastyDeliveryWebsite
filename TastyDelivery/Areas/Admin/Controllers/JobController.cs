using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.AdminModels;
using TastyDelivery.Core.Models.RestaurantModels;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Areas.Admin.Controllers
{
    public class JobController : AdminController
    {
        private const string Separator = "\r\n";
        private readonly IAdminService adminService;
        private readonly IRestaurantService restaurantService;
        private readonly IDeliveryManService deliveryManService;
        private readonly IOrderService orderService;
        public JobController(IAdminService _adminService,
            IRestaurantService _restaurantService, 
            IDeliveryManService _deliveryManService,
            IOrderService _orderService)
        {
            adminService = _adminService;
            restaurantService = _restaurantService;
            deliveryManService = _deliveryManService;
            orderService = _orderService;
        }

        [HttpGet]
        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRestaurant(AddRestaurantFormViewModel model)
        {
            if (model.Name == null || model.WorkingHours == null || model.Location == null || model.Type == null)
            {
                ModelState.AddModelError("", "Please fill out all required fields.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                adminService.CreateRestaurant(model);

                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult AddMenu()
        {
            var model = restaurantService.GetAllRestaurants();

            var modelToPass = new RestaurantModel
            {
                Restaurants = model.ToList()
            };

            return View(modelToPass);
        }

        [HttpPost]
        public IActionResult AddMenu(RestaurantModel model)
        {
            if(!ModelState.IsValid)
            {
                if(model.RestaurantId == 0)
                {
                    ModelState.AddModelError("Restaurants", "No restaurant selected");
                }


                model.Restaurants = restaurantService.GetAllRestaurants().ToList();

                return View(model);
            }
            
            string[] products = model.MenuItems.Split(Separator);

            foreach (var product in products)
            {
                string[] items = product.Split(',');

                string name = items[0];
                string description = items[1];
                double price = double.Parse(items[2]);
                Enum.TryParse(items[3], true, out ProductCategory category);

                var productToCreate = adminService.CreateProduct(model.RestaurantId, name, description, category, price);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AppointDriver()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AppointDriver(AppointDriverModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await adminService.CreateDriver(model);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CompletedDeliveries()
        {
            var model = adminService.GetCompletedDeliveries();

            if (model == null)
            {
                return View();
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await restaurantService.GetRestaurantById(id);

            if (restaurantService.CheckForPendingOrders(id))
            {
                var orders = orderService.GetRestaurantsOrders(id);

                return View("PendingOrdersError", orders);
            }

            restaurantService.Delete(restaurant);

            return RedirectToAction("Restaurants", "Restaurant", new { area = "" });
        }
    }
}
