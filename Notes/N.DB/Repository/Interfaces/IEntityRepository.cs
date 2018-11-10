﻿using N.DB.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.DB.Repository.Interfaces
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        T Create();

        T Load(long id);

        void Save(T entity);

        void Delete(long id);

        IEnumerable<T> GetAll();
    }
}
