using Microsoft.AspNetCore.Mvc;

namespace TastyDelivery.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
