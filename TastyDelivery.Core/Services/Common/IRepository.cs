﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyDelivery.Core.Services.Common
{
    public interface IRepository
    {
        public IQueryable<T> All<T>() where T : class;

        public IQueryable<T> AllReadOnly<T>() where T : class;
    }
}
