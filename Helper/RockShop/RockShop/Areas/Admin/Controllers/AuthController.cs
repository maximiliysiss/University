using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockShop.Areas.Admin.ViewModels.Auth;
using RockShop.Properties;
using RockShop.Services;
using System.Threading.Tasks;

namespace RockShop.Areas.Admin.Controllers
{
    /// <summary>
    /// Авторизация
    /// </summary>
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        /// <summary>
        /// Обрабокта входа
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return View("Login", loginModel);

            if (!await authService.LoginAsync(loginModel.Login, loginModel.Password))
            {
                ModelState.AddModelError(string.Empty, Resource.LoginIncorrect);
                return View("Login", loginModel);
            }

            return RedirectToAction("Index", "Users");
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}
