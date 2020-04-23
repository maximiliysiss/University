using AuthAPI.Models.Controller;
using AuthAPI.Models.Database;
using AuthAPI.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Services
{
    /// <summary>
    /// Сервис для авторизации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<LoginResult> LoginAsync(LoginModel loginModel);
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        Task<LoginResult> RegisterAsync(RegisterModel registerModel);
        /// <summary>
        /// Обновить пароль
        /// </summary>
        /// <param name="passwordRestore"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<LoginResult> RestorePassword(PasswordRestore passwordRestore, string token);
        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<LoginResult> RefreshToken(string refreshToken, string token);
    }

    public class AuthService : IAuthService
    {
        private readonly IAuthDataProvider authDataProvider;
        private readonly ICryptService cryptService;
        private readonly ITokenService tokenService;

        public AuthService(IAuthDataProvider authDataProvider, ICryptService cryptService, ITokenService tokenService)
        {
            this.authDataProvider = authDataProvider;
            this.cryptService = cryptService;
            this.tokenService = tokenService;
        }

        public async Task<LoginResult> LoginAsync(LoginModel loginModel)
        {
            var exists = authDataProvider.Users.FirstOrDefault(x => x.Email == loginModel.Login && x.PasswordHash == cryptService.CreateHash(loginModel.Password));
            if (exists == null)
                return null;
            var res = await GenerateTokenAndResult(exists);
            await authDataProvider.SaveChangesAsync();
            return res;
        }

        public async Task<LoginResult> RefreshToken(string refreshToken, string token)
        {
            var tokenInfo = tokenService.GetPrincipalFromExpiredToken(token, false);
            var user = authDataProvider.Users.FirstOrDefault(x => x.Email == tokenInfo.Identity.Name);
            if (user == null || user.RefreshToken != refreshToken)
                return null;

            var res = await GenerateTokenAndResult(user);
            await authDataProvider.SaveChangesAsync();
            return res;
        }

        public async Task<LoginResult> RegisterAsync(RegisterModel registerModel)
        {
            var exists = authDataProvider.Users.FirstOrDefault(x => x.Email == registerModel.Email);
            if (exists != null)
                return null;

            var newUser = new User
            {
                Email = registerModel.Email,
                Nickname = registerModel.Nickname,
                PasswordHash = cryptService.CreateHash(registerModel.Password),
                Role = authDataProvider.Roles.FirstOrDefault(x => x.Name == "User")
            };

            authDataProvider.Languages.Load();

            authDataProvider.Add(newUser);
            await authDataProvider.SaveChangesAsync();
            var res = await GenerateTokenAndResult(newUser);
            return res;
        }

        public async Task<LoginResult> RestorePassword(PasswordRestore passwordRestore, string token)
        {
            var tokenInfo = tokenService.GetPrincipalFromExpiredToken(token);
            var exists = authDataProvider.Users.FirstOrDefault(x => x.Id == passwordRestore.UserId);
            if (exists == null || exists.Email != tokenInfo.Identity.Name || exists.PasswordHash != cryptService.CreateHash(passwordRestore.PrevPassword))
                return null;

            exists.PasswordHash = cryptService.CreateHash(passwordRestore.Password);

            var res = await GenerateTokenAndResult(exists);
            await authDataProvider.SaveChangesAsync();
            return res;
        }

        private async Task<LoginResult> GenerateTokenAndResult(User user)
        {
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;
            await authDataProvider.SaveChangesAsync();

            var claimsToken = tokenService.GenerateToken(user);

            return new LoginResult
            {
                AccessToken = claimsToken,
                RefreshToken = refreshToken
            };
        }
    }
}
