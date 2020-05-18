using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Models.Controller;
using AuthAPI.Models.Database;
using AuthAPI.Services;
using CommonCoreLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthDataProvider db;
        private readonly IMapperService mapperService;

        public UsersController(IAuthDataProvider db, IMapperService mapperService)
        {
            this.db = db;
            this.mapperService = mapperService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> Index()
        {
            return (await db.Users.ToListAsync()).Select(x => mapperService.Map<UserViewModel>(x)).ToList();
        }

        [HttpGet("Find")]
        public async Task<ActionResult<UserViewModel>> Find(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id.ToString() == id);
            if (user == null)
                return NotFound();
            return mapperService.Map<UserViewModel>(user);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserViewModel>> Create(UserViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await db.Users.AnyAsync(x => x.Email == loginViewModel.Login))
                    return BadRequest("Такой пользователь уже существует");
                var role = db.Roles.FirstOrDefault(x => x.Name == loginViewModel.Role);
                var lang = db.Languages.First();
                var newUser = new User
                {
                    Email = loginViewModel.Login,
                    Nickname = loginViewModel.Login,
                    Role = role,
                    Language = lang,
                    LanguageId = lang.Id,
                    RoleId = role.Id
                };
                db.Add(newUser);
                db.SaveChanges();
                return Ok();
            }
            return loginViewModel;
        }


        [HttpPut("Edit")]
        public async Task<ActionResult<UserViewModel>> Edit(string id, UserViewModel loginViewModel)
        {
            if (id != loginViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id);
                var role = db.Roles.FirstOrDefault(x => x.Name == loginViewModel.Role);
                user.Email = loginViewModel.Login;
                user.Role = role;
                user.PasswordHash = CryptService.CreateHash(loginViewModel.Password);
                db.Update(user);
                db.SaveChanges();
                return Ok();
            }
            return loginViewModel;
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteConfirmed([FromBody]string toDelete)
        {
            var loginViewModel = await db.Users.FirstOrDefaultAsync(x => x.Id.ToString() == toDelete);
            if (loginViewModel == null)
                return NotFound();
            db.Delete(loginViewModel);
            db.SaveChanges();
            return Ok();
        }
    }
}