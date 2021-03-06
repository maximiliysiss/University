﻿using Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Services
{
    public interface ILoginService
    {
        User LoginAttempt(string login, string password);
    }

    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public class LoginService : ILoginService
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly DatabaseContext databaseContext;

        public LoginService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        /// <summary>
        /// Попытка получить пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
