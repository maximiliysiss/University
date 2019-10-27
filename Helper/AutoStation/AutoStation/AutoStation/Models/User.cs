using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AutoStation.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Token { get; set; }

        [NotMapped]
        public ClaimsIdentity ClaimsIdentity => new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Login) });
    }
}