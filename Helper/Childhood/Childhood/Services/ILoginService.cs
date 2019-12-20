using Childhood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Childhood.Services
{
    public interface ILoginService
    {
        /// <summary>
        /// Автторизация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User LoginAttempt(string login, string password);
    }

    public class LoginService : ILoginService
    {
        private readonly DatabaseContext databaseContext;

        public LoginService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public User LoginAttempt(string login, string password)
        {
            try
            {
                return App.user = databaseContext.Users.FirstOrDefault(x => x.Login == login && x.PasswordHash == CryptService.CreateMD5(password));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
