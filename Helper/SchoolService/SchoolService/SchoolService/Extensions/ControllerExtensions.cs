using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolService.Models;
using SchoolService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolService.Extensions
{
    public class UserContext : IDisposable
    {
        private readonly DatabaseContext databaseContext;
        public User User { get; set; }
        public DatabaseContext DatabaseContext => databaseContext;

        public UserContext(DatabaseContext databaseContext, User user)
        {
            this.databaseContext = databaseContext;
            User = user;
        }

        public void Dispose()
        {
            databaseContext.Dispose();
        }
    }


    public static class ControllerExtensions
    {
        public static UserContext GetUserContext(this ControllerBase controllerBase)
        {
            var context = (DatabaseContext)controllerBase.HttpContext.RequestServices.GetService(typeof(DatabaseContext));
            var login = controllerBase.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
            var res = context.Users.FirstOrDefault(x => x.Login == login);
            return new UserContext(context, res);
        }

        public static User GetCurrentUser(this ControllerBase controllerBase, DatabaseContext context)
        {
            var login = controllerBase.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
            return context.Users.FirstOrDefault(x => x.Login == login);
        }
    }
}
