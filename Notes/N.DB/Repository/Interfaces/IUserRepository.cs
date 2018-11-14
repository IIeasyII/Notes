using N.DB.Models;

namespace N.DB.Repository.Interfaces
{
    public interface IUserRepository : IEntityRepository<User>
    {
        /// <summary>
        /// Find user by login
        /// </summary>
        /// <param name="name">Login</param>
        /// <returns>User</returns>
        User LoadByLogin(string login);

        /// <summary>
        /// Registry new user
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        void RegistryUser(string login, string password);

        /// <summary>
        /// Find user id login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        long FindIdByLogin(string login);
    }
}