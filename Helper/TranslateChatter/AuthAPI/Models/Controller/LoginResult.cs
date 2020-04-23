namespace AuthAPI.Models.Controller
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class LoginResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
