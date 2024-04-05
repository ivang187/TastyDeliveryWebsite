using Microsoft.AspNetCore.Mvc;
using TastyDelivery.Areas.Admin.Models;
using TastyDelivery.Core.Contracts;

namespace TastyDelivery.Areas.Admin.Controllers
{
    public class JobController : AdminController
    {
        private readonly IAdminService adminService;
        public JobController(IAdminService _adminService)
        {
            adminService = _adminService;
        }

        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(AddRestaurantFormViewModel model)
        {
            var dbModel = await adminService.Create(model.Name, model.WorkingHours, model.Location);

            adminService.AddToDb(dbModel);

            return RedirectToAction("Index", "Home");
        }
    }
}
