namespace Flats.Models
{
    public enum UserType
    {
        Admin,
        User
    }

    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }
    }
}
