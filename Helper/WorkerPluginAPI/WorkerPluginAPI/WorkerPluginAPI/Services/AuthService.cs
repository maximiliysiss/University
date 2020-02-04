using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkerPluginAPI.Models;
using WorkerPluginAPI.Models.Controllers;
using WorkerPluginAPI.Services.Settings;

namespace WorkerPluginAPI.Services
{
    namespace AuthAPI.Services
    {
        /// <summary>
        /// Сервис авториазции
        /// </summary>
        public interface IAuthService
        {
            /// <summary>
            /// Авторизоваться
            /// </summary>
            /// <param name="loginModel"></param>
            /// <returns></returns>
            TokenResult AuthAttempt(LoginModel loginModel);
            /// <summary>
            /// Обновить токен
            /// </summary>
            /// <param name="token"></param>
            /// <param name="refreshToken"></param>
            /// <returns></returns>
            TokenResult RefreshToken(string token, string refreshToken);
            /// <summary>
            /// Сгенерировать токен для пользователя
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            string GenerateKey(Worker user);
        }

        public class AuthService : IAuthService
        {
            private readonly DatabaseContext databaseContext;
            private readonly AuthorizeSettings authorizeSettings;

            public AuthService(DatabaseContext databaseContext, AuthorizeSettings authorizeSettings)
            {
                this.databaseContext = databaseContext;
                this.authorizeSettings = authorizeSettings;
            }

            private string GenerateToken(Worker user)
            {
                var now = DateTime.Now;
                var jwt = new JwtSecurityToken(
                    issuer: authorizeSettings.Issuer,
                    audience: authorizeSettings.Audience,
                    claims: user.ClaimsIdentity.Claims,
                    notBefore: now,
                    expires: now.AddDays(authorizeSettings.Days),
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authorizeSettings.SecurityKey, SecurityAlgorithms.HmacSha256));
                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }

            public TokenResult AuthAttempt(LoginModel loginModel)
            {
                var user = databaseContext.Workers.FirstOrDefault(x => x.Login == loginModel.Login && x.PasswordHash == CryptService.CreateMD5(loginModel.Password));
                if (user == null)
                    return null;
                var token = GenerateToken(user);
                user.Token = CryptService.CreateMD5(token);
                databaseContext.Update(user);
                databaseContext.SaveChanges();

                return new TokenResult
                {
                    RefreshToken = user.Token,
                    AccessToken = token
                };
            }

            /// <summary>
            /// Вытащить информацию из токена
            /// </summary>
            /// <param name="token"></param>
            /// <returns></returns>
            private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidAudience = authorizeSettings.Audience,
                    ValidIssuer = authorizeSettings.Issuer,
                    IssuerSigningKey = authorizeSettings.SecurityKey
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }

            public TokenResult RefreshToken(string token, string refreshToken)
            {
                var principal = GetPrincipalFromExpiredToken(token);
                var user = databaseContext.Workers.FirstOrDefault(x => x.Login == principal.Identity.Name);
                if (user == null || user.Token != refreshToken)
                    return null;
                var accessToken = GenerateToken(user);
                user.Token = CryptService.CreateMD5(accessToken);

                databaseContext.Update(user);
                databaseContext.SaveChanges();

                return new TokenResult { AccessToken = accessToken, RefreshToken = user.Token };
            }

            public string GenerateKey(Worker user) => GenerateToken(user);
        }
    }

}
