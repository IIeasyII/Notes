using N.DB.Models;
using N.DB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.NHibernate.Repository
{
    public class NHUserRepository : NHBaseRepository<User>, IUserRepository
    {
        public User LoadByLogin(string login)
        {
            var session = NHibernateHelper.GetCurrentSession();

            var user = session.QueryOver<User>()
                .And(u => u.Login == login)
                .SingleOrDefault();

            NHibernateHelper.CloseSession();

            return user;
        }
    }
}
