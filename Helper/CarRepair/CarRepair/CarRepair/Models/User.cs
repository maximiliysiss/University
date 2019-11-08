using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Newtonsoft.Json;

namespace CarRepair.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string Token { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ClaimsIdentity ClaimsIdentity => new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Login) });
    }
}