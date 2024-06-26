﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.DeliveryManModels;
using TastyDelivery.Core.Services;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using TastyDelivery.Infrastructure.Utilities.Constants;

namespace TastyDelivery.Controllers
{
    [Authorize(Roles = UsersConstants.DeliveryManRoleName)]
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

        public IActionResult AssignedOrders()
        {
            var userId = GetUser();
            var model = deliveryManService.GetAssignedOrders(userId);

            if(model == null || !model.Any())
            {
                return View();
            }

            return View(model);
        }

   
        public async Task<IActionResult> TakeOrder(int orderId)
        {
            var userId = GetUser();

            await deliveryManService.AssignOrderToWorker(orderId, userId);

            return RedirectToAction(nameof(AssignedOrders));
        }

        public IActionResult OrderDelivered(int orderId)
        {
            deliveryManService.DeliverOrder(orderId);

            return RedirectToAction(nameof(Index));
        }

        private string GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }


    }
}
