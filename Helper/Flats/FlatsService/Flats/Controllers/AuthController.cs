using Flats.Models;
using Flats.Models.Controllers;
using Flats.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Flats.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly DatabaseContext databaseContext;

        public AuthController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<LoginResult> Login(LoginRegisterModel loginModel)
        {
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == loginModel.Login && x.PasswordHash == CryptService.GetMd5Hash(loginModel.Password));
            if (user == null)
                return NotFound();
            return new LoginResult { Role = user.UserType };
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<LoginResult> Register(LoginRegisterModel registerModel)
        {
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == registerModel.Login);
            if (user != null)
                return BadRequest();
            databaseContext.Add(user = new User { Login = registerModel.Login, PasswordHash = CryptService.GetMd5Hash(registerModel.Password), UserType = UserType.User });
            databaseContext.SaveChanges();
            return new LoginResult { Role = user.UserType }; ;
        }
    }
}