using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Роль
    /// </summary>
    public enum UserRole
    {
        Admin,
        User
    }

    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        /// <value></value>
        public string Login { get; set; }
        /// <summary>
        /// Хэш-Пароль
        /// </summary>
        /// <value></value>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        /// <value></value>
        public UserRole UserRole { get; set; }
        /// <summary>
        /// Токен
        /// </summary>
        /// <value></value>
        public string Token { get; set; }
        /// <summary>
        /// Информация об авторизации
        /// </summary>
        /// <param name="Claim(ClaimsIdentity.DefaultNameClaimType"></param>
        /// <returns></returns>
        [NotMapped]
        public ClaimsIdentity ClaimsIdentity =>
            new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRole.ToString()) });
    }
}
