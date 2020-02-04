using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace WorkerPluginAPI.Services.Settings
{
    /// <summary>
    /// Настройки авториазции
    /// </summary>
    public class AuthorizeSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
        public int AccessExpiration { get; set; }

        [JsonIgnore]
        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

        public int Days { get; set; }
    }
}
