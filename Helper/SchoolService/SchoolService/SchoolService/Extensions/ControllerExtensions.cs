using Microsoft.AspNetCore.Mvc;
using SchoolService.Models;
using SchoolService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolService.Extensions
{
    public static class ControllerExtensions
    {
        public static User GetCurrentUser(this ControllerBase controllerBase, DatabaseContext context)
        {
            var login = controllerBase.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
            return context.Users.FirstOrDefault(x => x.Login == login);
        }
    }
}
