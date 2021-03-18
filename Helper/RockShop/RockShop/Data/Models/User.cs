using System.ComponentModel.DataAnnotations;

namespace RockShop.Data.Models
{
    public class User
    {
        public long Id { get; set; }
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Display(Name = "Хеш пароля")]
        public string PasswordHash { get; set; }
    }
}
