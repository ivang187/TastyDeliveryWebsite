using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using TastyDelivery.Core.Models.Order;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        
        public OrderController(UserManager<ApplicationUser> _userManager) 
        {
            userManager = _userManager;
        }
        public async Task<IActionResult> Checkout(string cartData)
        {
            if (!String.IsNullOrEmpty(cartData))
            {
                var user = await GetUser();

                string decodedCartJson = WebUtility.UrlDecode(cartData);

                var cart = JsonConvert.DeserializeObject<Cart>(decodedCartJson);

                var products = cart.Products.Select(p => new CartItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                }).ToList();

                var order = new CheckoutViewModel
                {
                    Products = products,
                    User = user,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };

                return View(order);

            }

            return View();
        }

        private async Task<ApplicationUser> GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var user = await userManager.FindByIdAsync(userId);
                return user;
            }

            return null;
        }
    }
}
