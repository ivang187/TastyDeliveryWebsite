using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using TastyDelivery.Core.Models.Order;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository repository;
        private readonly TastyDeliveryDbContext teyDeliveryDbContext;
        
        public OrderController(UserManager<ApplicationUser> _userManager, IRepository _repository, TastyDeliveryDbContext _teyDeliveryDbContext) 
        {
            userManager = _userManager;
            repository = _repository;
            teyDeliveryDbContext = _teyDeliveryDbContext;
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
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.HomeAddress,
                    Phone = user.PhoneNumber,
                };

                return View(order);

            }

            return View();
        }
        public async Task<IActionResult> UserInfo(string firstName, string lastName, string address, string phone, string saveInfo, bool savePaymentInfo)
        {
            bool saveInfoBool = saveInfo == "on";
            var user = await GetUser();

            if (saveInfoBool)
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                user.HomeAddress = address;
                user.PhoneNumber = phone;

                repository.Update(user);
                repository.SaveChanges();
            }

            return RedirectToAction("CreateOrder", "Order");
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

        public async Task<IActionResult> CreateOrder()
        {
            return View();
        }


    }
}
