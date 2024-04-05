using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository repository;
        
        public AdminService(IRepository _repository) 
        {
            repository = _repository;
        }
        public async Task<Restaurant> Create(string name, string workingHours, string location)
        {
            var model = new Restaurant
            {
                Name = name,
                WorkingHours = workingHours,
                Location = location
            };

            return await Task.FromResult(model);
        }

        public void AddToDb(Restaurant restaurant)
        {
            repository.AddNew(restaurant);
            repository.SaveChanges();
        }


    }
}
