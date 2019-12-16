namespace Flats.Models
{
    /// <summary>
    /// Тип пользователя
    /// </summary>
    public enum UserType
    {
        Admin,
        User
    }

    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }
    }
}
