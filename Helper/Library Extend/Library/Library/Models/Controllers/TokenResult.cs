using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Controllers
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// Роль пользователя
        /// </summary>
        /// <value></value>
        public UserRole UserRole { get; set; }
        private string accessToken;
        /// <summary>
        /// Токен доступа
        /// </summary>
        /// <value></value>
        public string AccessToken
        {
            get => $"Bearer {accessToken}";
            set => accessToken = value;
        }
        private string refreshToken;
        /// <summary>
        /// Токен обновления
        /// </summary>
        /// <value></value>
        public string RefreshToken
        {
            get => $"Bearer {refreshToken}";
            set => refreshToken = value;
        }
    }
}
