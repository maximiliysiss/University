using Microsoft.AspNetCore.Http;
using System.Net.Http;
using TranslateChatter.Extensions;

namespace TranslateChatter.Services
{
    public class ChatAPIClient : ChatAPI.ChatAPIClient
    {
        public ChatAPIClient(string baseUrl, HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ITokenService tokenService) : base(baseUrl, httpClient)
        {
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                httpClient.DefaultRequestHeaders.Add("Authorization", tokenService.GenerateFullToken(httpContextAccessor.HttpContext.User.Token()));
        }
    }
}
