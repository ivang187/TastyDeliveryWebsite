using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data;

namespace TastyDelivery.Core.Services.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext context;

        public Repository(TastyDeliveryDbContext _context)
        {
            context = _context;
        }

        private DbSet<T> GetDbSet<T>() where T : class
        {
            return context.Set<T>();
        }

        public IQueryable<T> All<T>() where T : class
        {
            return GetDbSet<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return GetDbSet<T>().AsNoTracking();
        }
    }
}
