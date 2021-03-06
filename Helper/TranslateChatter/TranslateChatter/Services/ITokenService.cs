﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TranslateChatter.AuthAPI;
using TranslateChatter.Settings;

namespace TranslateChatter.Services
{
    /// <summary>
    /// Сервис для токена
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Получить инфу о пользователе из токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        /// <summary>
        /// Создать токен
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GenerateFullToken(string token);
        /// <summary>
        /// Авторизовать в системе
        /// </summary>
        /// <param name="loginResult"></param>
        /// <returns></returns>
        Task SignInAsync(LoginResult loginResult);
    }

    public class TokenService : ITokenService
    {
        private readonly AuthSettings authSettings;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TokenService(AuthSettings authSettings, IHttpContextAccessor httpContextAccessor)
        {
            this.authSettings = authSettings;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GenerateFullToken(string token) => $"Bearer {token}";

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = authSettings.Audience,
                ValidIssuer = authSettings.Issuer,
                IssuerSigningKey = authSettings.SecurityKey
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public async Task SignInAsync(LoginResult loginResult)
        {
            if (loginResult == null)
                return;
            var principal = GetPrincipalFromExpiredToken(loginResult.AccessToken);
            principal.Identities.First().AddClaim(new Claim("Token", loginResult.AccessToken));
            principal.Identities.First().AddClaim(new Claim("Refresh", loginResult.RefreshToken));
            await httpContextAccessor.HttpContext.SignOutAsync();
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties());
            /*for next request in this request*/
            httpContextAccessor.HttpContext.User = principal;
        }
    }
}
