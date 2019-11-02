namespace AutoStation.Models.Controller
{
    /// <summary>
    /// Изменение пароля
    /// </summary>
    public class UserChange
    {
        /// <summary>
        /// Логин
        /// </summary>
        /// <value></value>
        public string Login { get; set; }
        /// <summary>
        /// Новый пароль
        /// </summary>
        /// <value></value>
        public string Password { get; set; }
        /// <summary>
        /// Старый пароль
        /// </summary>
        /// <value></value>
        public string PasswordConfirm { get; set; }
    }
}