using Microsoft.AspNetCore.Authentication;
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
using TranslateChatter.Settings;

namespace TranslateChatter.Services
{
    public interface ITokenService
    {
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateFullToken(string token);
        Task SignInAsync(string token);
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

        public async Task SignInAsync(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            principal.Identities.First().AddClaim(new Claim("Token", token));
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties());
        }
    }
}
