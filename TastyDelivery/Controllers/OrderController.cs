using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.OrderModels;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository repository; 
        private readonly IOrderService orderService;

        public OrderController(UserManager<ApplicationUser> _userManager, 
            IRepository _repository, 
            IOrderService _orderService) 
        {
            userManager = _userManager;
            repository = _repository;
            orderService = _orderService;
        }
        public async Task<IActionResult> Checkout(string cartData)
        {
            if (!string.IsNullOrEmpty(cartData))
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
                    User = user,
                    Products = products,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.HomeAddress,
                    Phone = user.PhoneNumber,
                    RestaurantName = cart.RestaurantName,
                    Restaurant = await repository.AllReadOnly<Restaurant>().Where(r => r.Name == cart.RestaurantName).FirstOrDefaultAsync()
                };

                return View(order);

            }

            return View();
        }
        public async Task<IActionResult> UserInfo(CheckoutViewModel model, string saveInfo, bool savePaymentInfo)
        {
            var productsJson = HttpContext.Request.Form["ProductsData"];
            model.Products = JsonConvert.DeserializeObject<List<CartItemViewModel>>(productsJson);

            var restaurantJson = HttpContext.Request.Form["RestaurantData"];
            model.Restaurant = JsonConvert.DeserializeObject<Restaurant>(restaurantJson);
            model.RestaurantName = model.Restaurant.Name;
            model.User = await GetUser();

            CheckSaveInfo(model, saveInfo, model.User);

            var order = await orderService.CreateOrder(model);
            HttpContext.Session.Remove($"Cart_{User.FindFirst(ClaimTypes.NameIdentifier)?.Value}");

            return RedirectToAction("OrderDetails", "Order");
        }

        public async Task<IActionResult> OrderDetails()
        {
            var user = await GetUser();
            var myOrders = orderService.GetUserOrders(user);

            return View(myOrders);
        }

        private void CheckSaveInfo(CheckoutViewModel model, string saveInfo, ApplicationUser user)
        {
            bool saveInfoBool = saveInfo == "on";

            if (saveInfoBool)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.HomeAddress = model.Address;
                user.PhoneNumber = model.Phone;

                repository.Update(user);
            }
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
