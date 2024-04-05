using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Core.Contracts
{
    public interface IAdminService
    {
        public Task<Restaurant> Create(string name, string workingHours, string location);

        public void AddToDb(Restaurant restaurant);
    }
}
