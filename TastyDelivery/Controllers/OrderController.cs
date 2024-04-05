using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TastyDelivery.Core.Models.ShoppingCart;

namespace TastyDelivery.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Checkout(string cartData)
        {
            if (!String.IsNullOrEmpty(cartData))
            {
                string decodedCartJson = WebUtility.UrlDecode(cartData);

                var cart = JsonConvert.DeserializeObject<Cart>(decodedCartJson);

                return View(cart);
            }

            return View();
        }
    }
}
