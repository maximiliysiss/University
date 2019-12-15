using Flats.Models;
using Flats.Models.Controllers;
using Flats.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Flats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public AuthController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public ActionResult Login(LoginRegisterModel loginModel)
        {
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == loginModel.Login && x.PasswordHash == CryptService.GetMd5Hash(loginModel.Password));
            if (user == null)
                return NotFound();
            return Ok();
        }

        [HttpPost]
        public ActionResult Register(LoginRegisterModel registerModel)
        {
            var user = databaseContext.Users.FirstOrDefault(x => x.Login == registerModel.Login);
            if (user != null)
                return BadRequest();
            databaseContext.Add(new User { Login = registerModel.Login, PasswordHash = CryptService.GetMd5Hash(registerModel.Password), UserType = UserType.User });
            databaseContext.SaveChanges();
            return Ok();
        }
    }
}