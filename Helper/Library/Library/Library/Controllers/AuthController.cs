using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.Models;
using Library.Models.Controllers;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Library.Controllers
{
    /// <summary>
    /// Контроллер для авторизации
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        DatabaseContext databaseContext;
        /// <summary>
        /// Настройки авторизации
        /// </summary>
        AuthorizeSettings authorizeSettings;

        public AuthController(DatabaseContext databaseContext, AuthorizeSettings authorizeSettings)
        {
            this.databaseContext = databaseContext;
            this.authorizeSettings = authorizeSettings;
        }

        /// <summary>
        /// Создать токен
        /// </summary>
        /// <param name="user"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        private string GenerateToken(User user, int days)
        {
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: authorizeSettings.Issuer,
                audience: authorizeSettings.Audience,
                claims: user.ClaimsIdentity.Claims,
                notBefore: now,
                expires: now.AddDays(days),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authorizeSettings.SecurityKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<TokenResult> Login(LoginModel loginModel)
        {
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == loginModel.Login
                                                        && x.PasswordHash == CryptService.CreateMd5(loginModel.Password));
            if (user == null)
                return NotFound();

            user.Token = GenerateToken(user, authorizeSettings.RefreshExpiration);
            databaseContext.Update(user);
            databaseContext.SaveChanges();

            return new TokenResult
            {
                RefreshToken = user.Token,
                AccessToken = GenerateToken(user, authorizeSettings.AccessExpiration),
                UserRole = user.UserRole
            };
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<TokenResult> Register(LoginModel loginModel)
        {
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == loginModel.Login);
            if (user != null)
                return BadRequest();

            user = new User
            {
                Login = loginModel.Login,
                PasswordHash = CryptService.CreateMd5(loginModel.Password),
                UserRole = UserRole.User
            };
            databaseContext.Add(user);
            databaseContext.SaveChanges();

            return Login(loginModel);
        }

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<TokenResult> Refresh()
        {
            var token = Request.Headers["token"];
            var refreshToken = Request.Headers["refresh"];
            if (token.Count != 1 || refreshToken.Count != 1)
                return BadRequest();

            var principal = GetPrincipalFromExpiredToken(token);
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == principal.Identity.Name);
            if (user == null || user.Token != refreshToken)
                return BadRequest();
            var newJwt = GenerateToken(user, authorizeSettings.AccessExpiration);
            user.Token = GenerateToken(user, authorizeSettings.RefreshExpiration);

            databaseContext.Update(user);
            databaseContext.SaveChanges();

            return new TokenResult { AccessToken = newJwt, RefreshToken = user.Token, UserRole = user.UserRole };
        }

        /// <summary>
        /// Получить информацию из токена
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

        /// <summary>
        /// Попробовать войти по авторизации
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Try() => Ok();
    }
}