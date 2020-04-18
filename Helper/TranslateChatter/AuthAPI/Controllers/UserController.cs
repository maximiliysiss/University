using AuthAPI.Models.Controller;
using AuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapperService mapperService;
        private readonly IAuthDataProvider authDataProvider;

        public UserController(IUserService userService, IMapperService mapperService, IAuthDataProvider authDataProvider)
        {
            this.userService = userService;
            this.mapperService = mapperService;
            this.authDataProvider = authDataProvider;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<UserModel> GetUser([FromHeader]string authorization)
        {
            var parts = authorization.Split(" ");
            return mapperService.Map<UserModel>(userService.GetUser(parts[1]));
        }

        [Authorize]
        [HttpPut("changelanguage")]
        public ActionResult<UserModel> UpdateLanguage([FromBody]ChangeLanguageModel changeLanguageModel, [FromHeader]string authorization)
        {
            var parts = authorization.Split(" ");
            var user = userService.GetUser(parts[1]);
            authDataProvider.Update(user);
            authDataProvider.SaveChanges();
            return mapperService.Map<UserModel>(user);
        }
    }
}