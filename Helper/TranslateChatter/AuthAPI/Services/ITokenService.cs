﻿using AuthAPI.Models.Database;
using AuthAPI.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthAPI.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }

    public class TokenService : ITokenService
    {
        private readonly AuthSettings authSettings;

        public TokenService(AuthSettings authSettings)
        {
            this.authSettings = authSettings;
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

        private Claim[] CreateClaimIdentity(User user) => new[] {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
            new Claim(ClaimsIdentity.DefaultIssuer, authSettings.Issuer),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/salt", Guid.NewGuid().ToString()),
            new Claim("Code", user.Language.Code),
            new Claim("UICode", user.Language.UICode),
            new Claim("Id", user.Id.ToString())
        };
    }
}
