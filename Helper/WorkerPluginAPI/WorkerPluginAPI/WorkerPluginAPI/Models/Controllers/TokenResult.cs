namespace WorkerPluginAPI.Models.Controllers
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class TokenResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
