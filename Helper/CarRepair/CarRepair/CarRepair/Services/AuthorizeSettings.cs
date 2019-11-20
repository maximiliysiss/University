using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepair.Services
{
    /// <summary>
    /// Настройки авторизации
    /// </summary>
    public class AuthorizeSettings
    {
        /// <summary>
        /// Слушатель
        /// </summary>
        /// <value></value>
        public string Audience { get; set; }
        /// <summary>
        /// Издатель
        /// </summary>
        /// <value></value>
        public string Issuer { get; set; }
        /// <summary>
        /// Ключ
        /// </summary>
        /// <value></value>
        public string Key { get; set; }
        /// <summary>
        /// Время существавания токена обновления
        /// </summary>
        /// <value></value>
        public int RefreshExpiration { get; set; }
        /// <summary>
        /// Время существавания токена доступа
        /// </summary>
        /// <value></value>
        public int AccessExpiration { get; set; }
        /// <summary>
        /// Симметричный ключ
        /// </summary>
        /// <returns></returns>
        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}