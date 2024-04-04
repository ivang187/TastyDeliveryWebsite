using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services;

namespace TastyDelivery.Controllers
{
    public class ShoppingCart : Controller
    {
        private IShoppingCartService shoppingCartService;


        public ShoppingCart(IShoppingCartService _shoppingCartService) 
        {
            shoppingCartService = _shoppingCartService;
        }

        public IActionResult GetShoppingCart()
        {
            if (this.HttpContext.Session.GetString(GetUserSession()) == null)
            {
                var cart = new Cart();
                var cartJson = JsonConvert.SerializeObject(cart);
                this.HttpContext.Session.SetString(GetUserSession() , cartJson);
            }

            var cartData = this.HttpContext.Session.GetString(GetUserSession());
            var cartToPass = JsonConvert.DeserializeObject<Cart>(cartData);

            return View(cartToPass);

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] dynamic data)
        {
            try
            {
                int productId = data.GetProperty("productId").GetInt32();
                double price = data.GetProperty("price").GetDouble();
                int quantity = data.GetProperty("quantity").GetInt32();

                var model = await shoppingCartService.AddToCart(productId, price, quantity);

                var cartJson = this.HttpContext.Session.GetString(GetUserSession());
                Cart cart = cartJson != null ? JsonConvert.DeserializeObject<Cart>(cartJson) : new Cart();

                var existingItem = cart.Products.FirstOrDefault(item => item.Id == model.Id);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    cart.Products.Add(model);
                }

                this.HttpContext.Session.SetString(GetUserSession(), JsonConvert.SerializeObject(cart));

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private string GetUserSession()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            string sessionKey = $"Cart_{userId}";

            return sessionKey;
        }
    }
}
