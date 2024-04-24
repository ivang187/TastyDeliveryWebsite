using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Infrastructure.Data;
using TastyDelivery.Infrastructure.Data.Models;

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

        public void AddNew<T>(T entity) where T : class
        {
            GetDbSet<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {

            GetDbSet<T>().Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            GetDbSet<T>().Remove(entity);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }    
    }
}
