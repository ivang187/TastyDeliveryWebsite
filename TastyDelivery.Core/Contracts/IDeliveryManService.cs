using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.DeliveryManModels;
using TastyDelivery.Core.Models.OrderModels;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Core.Contracts
{
    public interface IDeliveryManService
    {
        public List<OrderDetailsViewModel> GetPendingDeliveries();

        public Task AssignOrderToWorker(int orderId, string userId);

        public AssignedOrdersViewModel CreateAssignedOrderModel(int orderId, List<OrderProducts> products);

        public List<AssignedOrdersViewModel> GetAssignedOrders(string userId);

        public void DeliverOrder(int orderId);
    }
}
