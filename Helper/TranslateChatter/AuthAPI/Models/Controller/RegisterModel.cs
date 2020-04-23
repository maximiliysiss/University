using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.Controller
{
    /// <summary>
    /// Модель регистрации
    /// </summary>
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Nickname { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
