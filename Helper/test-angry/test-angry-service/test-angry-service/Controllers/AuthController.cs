using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test_angry_service.Services;

namespace test_angry_service.Controllers
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

        [Authorize]
        public ActionResult Login()
        {
            return Ok();
        }

        [Authorize]
        public ActionResult Register()
        {
            return Ok();
        }
    }
}