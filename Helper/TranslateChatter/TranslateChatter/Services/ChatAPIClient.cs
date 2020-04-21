using Microsoft.AspNetCore.Http;
using System.Net.Http;
using TranslateChatter.AuthAPI;
using TranslateChatter.Extensions;
using TranslateChatter.Services;

namespace TranslateChatter.ChatAPI
{
    public partial class ChatAPIClient : APIClient
    {
        public ChatAPIClient(string baseUrl, HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IAuthAPIClient authAPIClient) : base(httpContextAccessor, tokenService)
        {
            this._baseUrl = baseUrl;
            this._httpClient = httpClient;
            AuthAPIClient = authAPIClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            base.PrepareRequest(client, request, url);
        }
    }
}
