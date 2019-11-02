namespace AutoStation.Models.Controller
{
    /// <summary>
    /// Результат входа
    /// </summary>
    public class TokenResult
    {
        private string accessToken;
        public string AccessToken
        {
            get => $"Bearer {accessToken}";
            set => accessToken = value;
        }
        private string refreshToken;
        public string RefreshToken
        {
            get => $"Bearer {refreshToken}";
            set => refreshToken = value;
        }
    }
}