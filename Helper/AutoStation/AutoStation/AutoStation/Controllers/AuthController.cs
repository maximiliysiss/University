using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoStation.Models.Controller;
using AutoStation.Services;
using AutoStation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace AutoStation.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        DatabaseContext databaseContext;
        /// <summary>
        /// Настройки авториазции
        /// </summary>
        AuthorizeSettings authorizeSettings;

        public AuthController(DatabaseContext databaseContext, AuthorizeSettings authorizeSettings)
        {
            this.databaseContext = databaseContext;
            this.authorizeSettings = authorizeSettings;
        }

        /// <summary>
        /// Сгенерировать токен
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
                AccessToken = GenerateToken(user, authorizeSettings.AccessExpiration)
            };
        }

        /// <summary>
        /// Обновить токены
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

            return new TokenResult { AccessToken = newJwt, RefreshToken = user.Token };
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
        /// Изменить пользователя
        /// </summary>
        /// <param name="userChange"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult UserChange(UserChange userChange)
        {
            var idClaim = this.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
            if (idClaim == null)
                return NotFound();
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == idClaim.Value);
            if (user == null || user.PasswordHash != CryptService.CreateMd5(userChange.PasswordConfirm))
                return BadRequest();
            if (!string.IsNullOrEmpty(userChange.Login))
                user.Login = userChange.Login;
            if (!string.IsNullOrEmpty(userChange.Password))
                user.PasswordHash = CryptService.CreateMd5(userChange.Password);
            databaseContext.Update(user);
            databaseContext.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Получить текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<User> CurrentUser()
        {
            var idClaim = this.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
            if (idClaim == null)
                return NotFound();
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == idClaim.Value);
            if (user == null)
                return BadRequest();
            return user;
        }

        /// <summary>
        /// Проверка авторизации
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Try() => Ok();
    }
}