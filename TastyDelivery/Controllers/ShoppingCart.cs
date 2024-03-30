using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models;

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
            if (this.HttpContext.Session.GetString("Cart") == null)
            {
                var cart = new Cart();
                var cartJson = JsonConvert.SerializeObject(cart);
                this.HttpContext.Session.SetString("Cart", cartJson);
            }

            var cartData = this.HttpContext.Session.GetString("Cart");
            var cartToPass = JsonConvert.DeserializeObject<Cart>(cartData);

            return View(cartToPass);

        }

        public async Task<IActionResult> Add(int restaurantId, int productId, double price, int quantity)
        {
            var model = await shoppingCartService.AddToCart(productId, price, quantity);

            var cartJson = this.HttpContext.Session.GetString("Cart");
            Cart cart = cartJson != null ? JsonConvert.DeserializeObject<Cart>(cartJson) : new Cart();

            cart.Products.Add(model);

            this.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("ShowMenu", "Restaurant" , new { id = restaurantId });
        }
    }
}
