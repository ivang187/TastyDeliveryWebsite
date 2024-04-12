using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TastyDelivery.Core.Services.Common
{
    public interface IRepository
    {
        public IQueryable<T> All<T>() where T : class;

        public IQueryable<T> AllReadOnly<T>() where T : class;

        public void AddNew<T>(T entity) where T : class;

        public void Update<T>(T entity) where T : class;

        public Task SaveChanges();
    }
}
