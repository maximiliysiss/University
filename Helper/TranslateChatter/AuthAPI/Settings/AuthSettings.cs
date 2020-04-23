using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthAPI.Settings
{
    /// <summary>
    /// Настройки для авторизации
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// Аудитория
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Издатель
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Ключ
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Время существоввания
        /// </summary>
        public int AccessExpiration { get; set; }
        public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
