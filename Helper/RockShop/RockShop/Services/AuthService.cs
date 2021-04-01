using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RockShop.Data;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RockShop.Services
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Войти
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> LoginAsync(string login, string password);
        /// <summary>
        /// Выйти
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();
    }

    public class AuthService : IAuthService
    {
        private readonly ICryptService cryptService;
        private readonly DatabaseContext databaseContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(ICryptService cryptService, DatabaseContext databaseContext, IHttpContextAccessor httpContextAccessor)
        {
            this.cryptService = cryptService;
            this.databaseContext = databaseContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(string login, string password)
        {
            if (!await databaseContext.Users.AnyAsync(x => x.Login == login && x.PasswordHash == cryptService.CreateMD5(password)))
                return false;

            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, login) };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            return true;
        }

        public Task LogoutAsync() => httpContextAccessor.HttpContext.SignOutAsync();
    }
}
