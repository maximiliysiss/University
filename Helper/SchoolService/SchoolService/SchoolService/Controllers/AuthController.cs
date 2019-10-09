using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolService.Models.Controllers;
using SchoolService.Services;

namespace SchoolService.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        private readonly Authorize authorize;

        public AuthController(DatabaseContext databaseContext, Authorize authorize)
        {
            this.databaseContext = databaseContext;
            this.authorize = authorize;
        }

        [HttpPost]
        public ActionResult<AuthToken> Login(LoginModel loginModel)
        {
            var loginUser = databaseContext.Users.FirstOrDefault(x => x.Login == loginModel.Login && x.PasswordHash == CryptService.CreateMD5(loginModel.Password));
            if (loginUser != null)
                return NotFound();
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(issuer: authorize.Issuer, audience: authorize.Audience, claims: loginUser.ClaimsIdentity.Claims, notBefore: now, expires: now.AddDays(authorize.Day),
                signingCredentials: new SigningCredentials(authorize.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
            var jwtCrypt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AuthToken
            {
                Token = jwtCrypt,
                UserType = loginUser.UserType
            };
        }
    }
}