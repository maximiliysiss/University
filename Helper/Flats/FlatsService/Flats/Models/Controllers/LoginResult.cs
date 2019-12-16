namespace Flats.Models.Controllers
{
    /// <summary>
    /// Результат авторизации = Роль пользователя
    /// </summary>
    public class LoginResult
    {
        public UserType Role { get; set; }
    }
}