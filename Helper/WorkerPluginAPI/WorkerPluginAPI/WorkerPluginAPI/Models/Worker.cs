using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace WorkerPluginAPI.Models
{
    public enum WorkerType
    {
        Worker,
        Admin,
    }

    public class Worker
    {
        public int ID { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string Token { get; set; }
        public WorkerType WorkerType { get; set; }
        [NotMapped]
        [JsonProperty("PasswordHash")]
        public string PasswordHashJson { set => PasswordHash = value; }

        [JsonIgnore]
        [NotMapped]
        public ClaimsIdentity ClaimsIdentity => new ClaimsIdentity(new[] {
            new Claim(ClaimsIdentity.DefaultNameClaimType, Login),
            new Claim("UserIdentifier", ID.ToString()),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, WorkerType.ToString())
        });
    }
}
