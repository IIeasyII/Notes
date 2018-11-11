using N.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.DB.Repository.Interfaces
{
    public interface IUserRepository : IEntityRepository<User>
    {
        /// <summary>
        /// Найти пользователя по логину
        /// </summary>
        /// <param name="name">Логин пользователя</param>
        /// <returns>Пользователя</returns>
        User LoadByLogin(string login);

        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        void RegistryUser(string login, string password);

        long FindIdByLogin(string login);
    }
}
