using N.DB.Models.Interfaces;
using N.DB.Repository.Interfaces;
using NHibernate;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.NHibernate.Repository
{
    public class NHBaseRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        public virtual T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public virtual void Delete(long id)
        {
            var session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                var entity = Load(id);

                if (entity != null)
                {
                    session.Delete(entity);
                }
                session.Flush();
            }

        }

        public virtual IEnumerable<T> GetAll()
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            var entities = session.CreateCriteria<T>().List<T>();

            NHibernateHelper.CloseSession();

            return entities;
        }

        public virtual T Load(long id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                var user = session.Load<T>(id);

                return user;
            }
        }

        public virtual void Save(T entity)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                session.SaveOrUpdate(entity);
            }
        }
    }
}
