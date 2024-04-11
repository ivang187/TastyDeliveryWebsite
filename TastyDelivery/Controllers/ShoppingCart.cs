using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services;
using TastyDelivery.Core.Services.Common;

namespace TastyDelivery.Controllers
{
    public class ShoppingCart : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IRepository repository;
        private readonly IRestaurantService restaurantService;


        public ShoppingCart(IShoppingCartService _shoppingCartService, IRepository _repository, IRestaurantService _restaurantService) 
        {
            shoppingCartService = _shoppingCartService;
            repository = _repository;
            restaurantService = _restaurantService;
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
                string restaurantName = await restaurantService.GetRestaurantName(data.GetProperty("restaurantId").GetInt32());
                int productId = data.GetProperty("productId").GetInt32();
                double price = data.GetProperty("price").GetDouble();
                int quantity = data.GetProperty("quantity").GetInt32();

                var model = await shoppingCartService.FindItemToAdd(productId, price, quantity);

                var cart = GetCartSession();

                cart.RestaurantName = restaurantName;

                var existingItem = cart.Products.FirstOrDefault(item => item.Id == model.Id);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    cart.Products.Add(model);
                }

                UpdateCartSession(cart);    

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> Remove(int id)
        {
            var model = await shoppingCartService.FindItemToRemove(id);

            var cart = GetCartSession();

            var existingItem = cart.Products.FirstOrDefault(p => p.Id == model.Id);

            cart.Products.Remove(existingItem);

            UpdateCartSession(cart);

            return RedirectToAction(nameof(GetShoppingCart));
        }

        public IActionResult SendToCheckout()
        {
            var cart = GetCartSession();

            string cartJson = JsonConvert.SerializeObject(cart);
            string encodedCartJson = WebUtility.UrlEncode(cartJson);

            return RedirectToAction("Checkout", "Order", new { cartData = encodedCartJson });
        }

        private string GetUserSession()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            string sessionKey = $"Cart_{userId}";

            return sessionKey;
        }

        private Cart GetCartSession()
        {
            var cartJson = this.HttpContext.Session.GetString(GetUserSession());
            Cart cart = cartJson != null ? JsonConvert.DeserializeObject<Cart>(cartJson) : new Cart();

            return cart;
        }

        private Cart UpdateCartSession(Cart cart)
        {
            this.HttpContext.Session.SetString(GetUserSession(), JsonConvert.SerializeObject(cart));

            return cart;
        }
    }
}
