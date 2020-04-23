using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;
using TranslateChatter.Extensions;
using TranslateChatter.Services;

namespace TranslateChatter.AuthAPI
{
    /// <summary>
    /// АПИ клиент
    /// </summary>
    public class APIClient
    {
        /// <summary>
        /// Для получения контекста
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITokenService tokenService;
        public IAuthAPIClient AuthAPIClient { get; protected set; }
        /// <summary>
        /// Endpoint, которым не нужно проверять авторизацию
        /// </summary>
        private readonly string[] authEndPoints = new[] {
            "/api/Auth/refreshToken",
            "/api/Auth/login"
        };

        protected ClaimsPrincipal User => httpContextAccessor?.HttpContext?.User;

        public APIClient()
        {
            this.httpContextAccessor = null;
            this.tokenService = null;
        }

        public APIClient(IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// Проверим авторизацию и установим ее для запроса
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="url"></param>
        protected void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!url.IsContains(authEndPoints))
                {
                    try
                    {
                        AuthAPIClient.ApiAuthLoginGetAsync().GetAwaiter().GetResult();
                    }
                    catch (ApiException ex)
                    {
                        if (ex.StatusCode != 401)
                            throw;

                        var result = AuthAPIClient.ApiAuthRefreshtokenAsync(User.Refresh(), User.Token()).GetAwaiter().GetResult();
                        tokenService.SignInAsync(result);
                    }
                }

                client.DefaultRequestHeaders.Remove("Authorization");
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", tokenService.GenerateFullToken(httpContextAccessor.HttpContext.User.Token()));
                client.DefaultRequestHeaders.Add("Authorization", tokenService.GenerateFullToken(httpContextAccessor.HttpContext.User.Token()));
            }
        }
    }

    /// <summary>
    /// Клиент для AuthAPI
    /// </summary>
    public partial class AuthAPIClient : APIClient
    {
        public AuthAPIClient(string baseUrl, HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ITokenService tokenService) : base(httpContextAccessor, tokenService)
        {
            this._baseUrl = baseUrl;
            this._httpClient = httpClient;
            this.AuthAPIClient = this;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            base.PrepareRequest(client, request, url);
        }
    }
}
