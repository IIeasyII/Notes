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
        public long FindIdByLogin(string login)
        {
            var session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                var user = session.QueryOver<User>()
                .And(u => u.Login == login)
                .SingleOrDefault();

                return user.Id;
            }
        }

        public User LoadByLogin(string login)
        {
            var session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                var user = session.QueryOver<User>()
                .And(u => u.Login == login)
                .SingleOrDefault();

                return user;
            }
        }

        public void RegistryUser(string login, string password)
        {
            var session = NHibernateHelper.GetCurrentSession();
            using (session.BeginTransaction())
            {
                session.CreateSQLQuery("INSERT INTO [User] ([Login], [Password], [RoleId]) VALUES (:Login, :Password, :RoleId)")
                .SetString("Login", login)
                .SetString("Password", password)
                .SetInt32("RoleId", 2)
                .ExecuteUpdate();
            }
        }
    }
}
