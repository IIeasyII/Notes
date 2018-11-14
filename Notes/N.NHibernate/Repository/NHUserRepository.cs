using N.DB.Models;
using N.DB.Repository.Interfaces;

namespace N.NHibernate.Repository
{
    public class NHUserRepository : NHBaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// Find di user by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>User</returns>
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

        /// <summary>
        /// Find user by login
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>User</returns>
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

        /// <summary>
        /// Registry user by login and password as just user
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
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