using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Controllers
{
    /// <summary>
    /// Результат входа
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// Роль
        /// </summary>
        /// <value></value>
        public UserRole UserRole { get; set; }
        private string accessToken;
        /// <summary>
        /// Доступ
        /// </summary>
        /// <value></value>
        public string AccessToken
        {
            get => $"Bearer {accessToken}";
            set => accessToken = value;
        }
        /// <summary>
        /// Токен обновления
        /// </summary>
        /// <value></value>
        public string RefreshToken { get; set; }
    }
}
