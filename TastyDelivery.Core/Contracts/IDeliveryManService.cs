using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.DeliveryManModels;
using TastyDelivery.Core.Models.OrderModels;

namespace TastyDelivery.Core.Contracts
{
    public interface IDeliveryManService
    {
        public List<OrderDetailsViewModel> GetPendingDeliveries();

        public Task AssignOrderToWorker(int orderId, string userId);

        public Task<AssignedOrdersViewModel> CreateAssignedOrderModel(int orderId);

        public Task<List<AssignedOrdersViewModel>> GetAssignedOrders(string userId);

        public Task DeliverOrder(int orderId);
    }
}
