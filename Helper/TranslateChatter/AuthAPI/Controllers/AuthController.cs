using System.Threading.Tasks;
using AuthAPI.Models.Controller;
using AuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> LoginAsync([FromBody]LoginModel loginModel)
        {
            var res = await authService.LoginAsync(loginModel);
            if (res == null)
                return NotFound();
            return res;
        }

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpPut("refreshToken")]
        public async Task<ActionResult<LoginResult>> RefreshTokenAsync([FromHeader]string refreshToken, [FromHeader]string authorization)
        {
            var parts = authorization?.Split(" ");
            var res = await authService.RefreshToken(refreshToken, parts[1]);
            if (res == null)
                return NotFound();
            return res;
        }

        /// <summary>
        /// Проверить токен
        /// </summary>
        /// <returns></returns>
        [HttpGet("login")]
        [Authorize]
        public ActionResult Login() => Ok();

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<LoginResult>> RegisterAsync([FromBody]RegisterModel registerModel)
        {
            var res = await authService.RegisterAsync(registerModel);
            if (res == null)
                return BadRequest("This user is exists");
            return res;
        }

        /// <summary>
        /// Восстановить пароль
        /// </summary>
        /// <param name="passwordRestore"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpPut("restorepassword")]
        [Authorize]
        public async Task<ActionResult<LoginResult>> RestorePasswordAsync([FromBody] PasswordRestore passwordRestore, [FromHeader]string authorization)
        {
            var parts = authorization?.Split(" ");
            var res = await authService.RestorePassword(passwordRestore, parts[1]);
            if (res == null)
                return NotFound();
            return res;
        }
    }
}