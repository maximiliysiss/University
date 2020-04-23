using Microsoft.AspNetCore.Http;
using System.Threading;
using TranslateChatter.Extensions;

namespace TranslateChatter.Services
{
    /// <summary>
    /// Система для локализации
    /// </summary>
    public interface ILocalizer
    {
        string this[string code] { get; }
    }

    public class Localizer : ILocalizer
    {
        public Localizer(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(httpContextAccessor.HttpContext.User.UICode());
        }

        public string this[string code] => Properties.Resources.ResourceManager.GetString(code);
    }
}
