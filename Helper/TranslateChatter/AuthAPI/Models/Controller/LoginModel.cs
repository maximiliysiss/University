using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.Controller
{
    /// <summary>
    /// Логин
    /// </summary>
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
