using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Роли
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
        public string Login { get; set; }
        /// <summary>
        /// Хэш пароля
        /// </summary>
        /// <value></value>
        public string PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
        public string Token { get; set; }

        [NotMapped]
        public ClaimsIdentity ClaimsIdentity =>
            new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRole.ToString()) });
    }
}
