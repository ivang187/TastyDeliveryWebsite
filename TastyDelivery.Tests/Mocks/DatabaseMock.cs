using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data;

namespace TastyDelivery.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static TastyDeliveryDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<TastyDeliveryDbContext>()
                    .UseInMemoryDatabase("TastyDeliveryInMemoryDb" + DateTime.Now.Ticks.ToString())
                    .Options;
                    
                return new TastyDeliveryDbContext(dbContextOptions, false);
            }

        }
    }
}
