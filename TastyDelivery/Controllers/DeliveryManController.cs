using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.DeliveryManModels;
using TastyDelivery.Core.Services;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Controllers
{
    public class DeliveryManController : Controller
    {
        private readonly IDeliveryManService deliveryManService;

        public DeliveryManController(IDeliveryManService _deliveryManService)
        {
            deliveryManService = _deliveryManService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AvailableOrders()
        { 
            var model = deliveryManService.GetPendingDeliveries();

            if(model == null)
            {
                return View();
            }

            return View(model);
        }

        public async Task<IActionResult> AssignedOrders()
        {
            var userId = GetUser();
            var model = await deliveryManService.GetAssignedOrders(userId);

            return View(model);
        }

   
        public async Task<IActionResult> TakeOrder(int orderId)
        {
            var userId = GetUser();

            await deliveryManService.AssignOrderToWorker(orderId, userId);

            return RedirectToAction(nameof(AssignedOrders));
        }

        private string GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }


    }
}
