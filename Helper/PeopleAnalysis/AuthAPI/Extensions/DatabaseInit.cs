using AuthAPI.Models.Database;
using AuthAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Extensions
{
    public static class DatabaseInit
    {
        public static void InitDatabase(this IApplicationBuilder app)
        {
            var scopeService = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeService.CreateScope();
            var authProvider = scope.ServiceProvider.GetRequiredService<IAuthDataProvider>();
            var cryptService = scope.ServiceProvider.GetRequiredService<ICryptService>();

            if (!authProvider.Users.Any())
            {
                authProvider.Add(new User
                {
                    Email = "admin@admin.ru",
                    Language = authProvider.Languages.FirstOrDefault(),
                    Nickname = "admin",
                    PasswordHash = cryptService.CreateHash("admin"),
                    Role = authProvider.Roles.FirstOrDefault(x => x.Name == "Admin")
                });
                authProvider.SaveChanges();
            }
        }
    }
}
