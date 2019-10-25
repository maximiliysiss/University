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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        DatabaseContext databaseContext;
        AuthorizeSettings authorizeSettings;

        public AuthController(DatabaseContext databaseContext, AuthorizeSettings authorizeSettings)
        {
            this.databaseContext = databaseContext;
            this.authorizeSettings = authorizeSettings;
        }

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

        [HttpPost]
        public ActionResult<TokenResult> Register(LoginModel loginModel)
        {
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == loginModel.Login);
            if (user != null)
                return BadRequest();

            user = new User
            {
                Login = loginModel.Login,
                PasswordHash = CryptService.CreateMd5(loginModel.Password)
            };
            databaseContext.Add(user);
            databaseContext.SaveChanges();

            return Login(loginModel);
        }

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

        [Authorize]
        [HttpGet]
        public ActionResult Try() => Ok();
    }
}