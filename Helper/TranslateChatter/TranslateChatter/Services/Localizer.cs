using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TranslateChatter.Models;

namespace TranslateChatter.Services
{
    public interface ILocalizer
    {
        string this[string code] { get; }
    }

    public class Localizer : ILocalizer
    {
        private IHttpContextAccessor httpContextAccessor;

        public Localizer(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            var userName = httpContextAccessor.HttpContext.User?.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                //var uiCode = userManager.FindByEmailAsync(userName).Result.Language.UICode;
                //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(uiCode);
            }
        }

        public string this[string code] => Properties.Resources.ResourceManager.GetString(code);
    }
}
