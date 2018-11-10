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
        public override void Delete(long id)
        {
            /*var user = Load(id);

            if (user != null)
            {
                user.Status = UserStatus.DELETED;
                Save(user);
            }*/
        }

        public void Block(long id)
        {
            /*var user = Load(id);

            if (user != null)
            {
                user.Status = UserStatus.BLOCKED;
                Save(user);
            }*/
        }

        public User LoadByName(string name)
        {
            var session = NHibernateHelper.GetCurrentSession();

            var entity = session.QueryOver<User>().List();/*
                .And(u => u.Login == name)
                .SingleOrDefault();
                */
            NHibernateHelper.CloseSession();

            return null;
        }
    }
}
