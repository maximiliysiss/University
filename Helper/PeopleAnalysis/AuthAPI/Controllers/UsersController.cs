using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Models.Controller;
using AuthAPI.Models.Database;
using AuthAPI.Services;
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
        private readonly ICryptService cryptService;

        public UsersController(IAuthDataProvider db, IMapperService mapperService, ICryptService cryptService)
        {
            this.db = db;
            this.mapperService = mapperService;
            this.cryptService = cryptService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> Index()
        {
            return await db.Users.Select(x => mapperService.Map<UserModel>(x)).ToListAsync();
        }

        [HttpGet("Find")]
        public async Task<ActionResult<UserModel>> Find(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id.ToString() == id);
            if (user == null)
                return NotFound();
            return mapperService.Map<UserModel>(user);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserViewModel>> Create(UserViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await db.Users.AnyAsync(x => x.Email == loginViewModel.Login))
                    return BadRequest("Такой пользователь уже существует");
                var role = db.Roles.FirstOrDefault(x => x.Name == loginViewModel.Role);
                var newUser = new User { Email = loginViewModel.Login, Nickname = loginViewModel.Login, Role = role };
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
                user.PasswordHash = cryptService.CreateHash(loginViewModel.Password);
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