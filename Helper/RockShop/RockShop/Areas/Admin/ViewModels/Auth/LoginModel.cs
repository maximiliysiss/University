using System.ComponentModel.DataAnnotations;

namespace RockShop.Areas.Admin.ViewModels.Auth
{
    /// <summary>
    /// Модель входа
    /// </summary>
    public class LoginModel
    {
        [Required(ErrorMessage = "Логин обязательное поле")]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Пароль обязательное поле")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
