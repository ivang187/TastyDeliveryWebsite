using Microsoft.AspNetCore.Mvc;
using TastyDelivery.Areas.Admin.Models;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.RestaurantModels;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;

namespace TastyDelivery.Areas.Admin.Controllers
{
    public class JobController : AdminController
    {
        private const string Separator = "\r\n";
        private readonly IAdminService adminService;
        private readonly IRestaurantService restaurantService;
        private readonly IRepository repository;
        public JobController(IAdminService _adminService,
            IRestaurantService _restaurantService, 
            IRepository _repository)
        {
            adminService = _adminService;
            restaurantService = _restaurantService;
            repository = _repository;
        }

        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(AddRestaurantFormViewModel model)
        {
            var dbModel = await adminService.CreateRestaurant(model.Name, model.WorkingHours, model.Location);

            repository.AddNew(dbModel);
            await repository.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddMenu()
        {
            var model = await restaurantService.GetAllRestaurants();

            var modelToPass = model.Select(r => new RestaurantModel
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();

            return View(modelToPass);
        }

        public async Task<IActionResult> UpdateMenu(int restaurantId, string menuItems)
        {
            if (!string.IsNullOrEmpty(menuItems))
            {
                string[] products = menuItems.Split(Separator);

                foreach (var product in products)
                { 
                    string[] items = product.Split(',');

                    string name = items[0];
                    string description = items[1];
                    Enum.TryParse(items[2], true, out ProductCategory category);
                    double price = double.Parse(items[3]);

                    var restaurantName = await restaurantService.GetRestaurantName(restaurantId);

                    var productToCreate = adminService.CreateProduct(restaurantId, name, description, category, price);

                    repository.AddNew(productToCreate);
                    await repository.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AppointDriver()
        {
            return View();
        }

        public IActionResult ToWebsite()
        {
            return RedirectToAction("Restaurants", "Restaurant");
        }
    }
}
