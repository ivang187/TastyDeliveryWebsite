using Microsoft.AspNetCore.Mvc;

namespace TastyDelivery.Controllers
{
    public class DeliveryManController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
