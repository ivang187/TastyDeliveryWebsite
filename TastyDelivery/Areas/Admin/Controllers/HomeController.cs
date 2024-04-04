using Microsoft.AspNetCore.Mvc;

namespace TastyDelivery.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
