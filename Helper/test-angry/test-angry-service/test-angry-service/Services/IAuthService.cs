using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using test_angry_service.Models;
using test_angry_service.Models.Controllers;
using test_angry_service.Services.Settings;

namespace test_angry_service.Services
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(LoginModel loginModel);
        Task<LoginResult> RegisterAsync(RegisterModel registerModel);
        Task<LoginResult> RefreshToken(string refreshToken, string token);
    }

    public class AuthService : IAuthService
    {
        private readonly DatabaseContext databaseContext;
        private readonly ICryptService cryptService;
        private readonly AuthSettings authSettings;

        public AuthService(DatabaseContext databaseContext, ICryptService cryptService, AuthSettings authSettings)
        {
            this.databaseContext = databaseContext;
            this.cryptService = cryptService;
            this.authSettings = authSettings;
        }

        public async Task<LoginResult> LoginAsync(LoginModel loginModel)
        {
            var exists = databaseContext.Users.FirstOrDefault(x => x.Name == loginModel.Login && x.PasswordHash == cryptService.CreateHash(loginModel.Password));
            if (exists == null)
                return null;
            var res = await GenerateTokenAndResult(exists);
            await databaseContext.SaveChangesAsync();
            return res;
        }

        private async Task<LoginResult> GenerateTokenAndResult(User user)
        {
            var refreshToken = Guid.NewGuid().ToString();
            var claimsToken = GenerateToken(user);

            user.RefreshToken = refreshToken;
            await databaseContext.SaveChangesAsync();

            return new LoginResult
            {
                AccessToken = claimsToken,
                RefreshToken = refreshToken,
                Name = user.Name
            };
        }

        public string GenerateToken(User user)
        {
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: authSettings.Issuer,
                audience: authSettings.Audience,
                claims: CreateClaimIdentity(user),
                notBefore: now,
                expires: now.AddMinutes(authSettings.AccessExpiration),
                signingCredentials: new SigningCredentials(authSettings.SecurityKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private Claim[] CreateClaimIdentity(User user) => new[] {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, "User"),
            new Claim(ClaimsIdentity.DefaultIssuer, authSettings.Issuer),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/salt", Guid.NewGuid().ToString())
        };

        public async Task<LoginResult> RefreshToken(string refreshToken, string token)
        {
            var tokenInfo = GetPrincipalFromExpiredToken(token);
            var user = databaseContext.Users.FirstOrDefault(x => x.Name == tokenInfo.Identity.Name);
            if (user == null || user.RefreshToken != refreshToken)
                return null;

            var res = await GenerateTokenAndResult(user);
            await databaseContext.SaveChangesAsync();
            return res;
        }

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

        public async Task<LoginResult> RegisterAsync(RegisterModel registerModel)
        {
            var exists = databaseContext.Users.FirstOrDefault(x => x.Name == registerModel.Nickname);
            if (exists != null)
                return null;

            var newUser = new User
            {
                Name = registerModel.Nickname,
                PasswordHash = cryptService.CreateHash(registerModel.Password)
            };

            databaseContext.Add(newUser);
            var res = await GenerateTokenAndResult(newUser);
            await databaseContext.SaveChangesAsync();
            return res;
        }
    }
}
