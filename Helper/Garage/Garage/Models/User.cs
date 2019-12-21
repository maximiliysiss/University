using Garage.Extensions.Attributes;

namespace Garage.Models
{
    public enum UserRole
    {
        User,
        HomeKeeper
    }

    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        [HideColumn]
        public int ID { get; set; }
        [DisplayGridName("Логин")]
        public string Login { get; set; }
        [HideColumn]
        public string PasswordHash { get; set; }
        [HideColumn]
        public UserRole UserRole { get; set; } = UserRole.User;
        public override string ToString() => Login;
    }
}
