using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using test_angry_service.Models.Controllers;
using test_angry_service.Services;

namespace test_angry_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> LoginAsync([FromBody]LoginModel loginModel)
        {
            var res = await authService.LoginAsync(loginModel);
            if (res == null)
                return NotFound();
            return res;
        }

        [Authorize]
        [HttpPost("refreshToken")]
        public async Task<ActionResult<LoginResult>> RefreshTokenAsync([FromHeader]string refreshToken, [FromHeader]string authorization)
        {
            var parts = authorization?.Split(" ");
            var res = await authService.RefreshToken(refreshToken, parts[1]);
            if (res == null)
                return NotFound();
            return res;
        }

        [HttpGet("login")]
        [Authorize]
        public ActionResult Login() => Ok();

        [HttpPost("register")]
        public async Task<ActionResult<LoginResult>> RegisterAsync([FromBody]RegisterModel registerModel)
        {
            var res = await authService.RegisterAsync(registerModel);
            if (res == null)
                return BadRequest("This user is exists");
            return res;
        }
    }
}