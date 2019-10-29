using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using SchoolService.Models;
using SchoolService.Models.Controllers;
using SchoolService.Services;
using SchoolService.Extensions;

namespace SchoolService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        private readonly Authorize authorize;

        private string GenerateToken(User user, int days)
        {
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: authorize.Issuer,
                audience: authorize.Audience,
                claims: user.ClaimsIdentity.Claims,
                notBefore: now,
                expires: now.AddDays(days),
                signingCredentials: new SigningCredentials(authorize.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public AuthController(DatabaseContext databaseContext, Authorize authorize)
        {
            this.databaseContext = databaseContext;
            this.authorize = authorize;
        }

        [HttpPost]
        public ActionResult<AuthToken> Login(LoginModel loginModel)
        {
            if (loginModel == null || loginModel.Login == null || loginModel.Password == null)
                return NotFound();
            var loginUser = databaseContext.Users.FirstOrDefault(x => x.Login == loginModel.Login && x.PasswordHash == CryptService.CreateMD5(loginModel.Password));
            if (loginUser == null)
                return NotFound();

            loginUser.Token = GenerateToken(loginUser, authorize.LongDay);
            databaseContext.Update(loginUser);
            databaseContext.SaveChanges();

            return new AuthToken
            {
                AccessToken = GenerateToken(loginUser, authorize.ShortDay),
                RefreshToken = loginUser.Token,
                UserType = loginUser.UserType,
                Id = loginUser.ID
            };
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangeUser changeUser)
        {
            var user = this.GetCurrentUser(databaseContext);
            if (user.PasswordHash != CryptService.CreateMD5(changeUser.PasswordConfirm))
                return BadRequest();
            if (!string.IsNullOrEmpty(changeUser.Login))
                user.Login = changeUser.Login;
            if (!string.IsNullOrEmpty(changeUser.Password))
                user.PasswordHash = CryptService.CreateMD5(changeUser.Password);
            databaseContext.Update(user);
            databaseContext.SaveChanges();
            return Ok();
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = authorize.Audience,
                ValidIssuer = authorize.Issuer,
                IssuerSigningKey = authorize.SymmetricSecurityKey
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        [HttpGet]
        public ActionResult<AuthToken> RefreshToken()
        {
            var token = this.Request.Headers["token"];
            var refresh = this.Request.Headers["refresh"];

            if (token.Count != 1 || refresh != 1)
                return NotFound();

            var principal = GetPrincipalFromExpiredToken(token);
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == principal.Identity.Name);
            if (user == null || user.Token != refresh)
                return null;
            var newJwt = GenerateToken(user, authorize.ShortDay);
            user.Token = GenerateToken(user, authorize.LongDay);

            databaseContext.Update(user);
            databaseContext.SaveChanges();

            return new AuthToken { AccessToken = newJwt, RefreshToken = user.Token, UserType = user.UserType };
        }

        [HttpGet]
        [Authorize]
        public ActionResult Try()
        {
            return Ok();
        }
    }
}