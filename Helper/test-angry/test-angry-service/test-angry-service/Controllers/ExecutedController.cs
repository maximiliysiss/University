using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test_angry_service.Models;
using test_angry_service.Models.Controllers;
using test_angry_service.Services;

namespace test_angry_service.Controllers
{
    /// <summary>
    /// Log
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExecutedController : ControllerBase
    {
        private DatabaseContext context;

        public ExecutedController(DatabaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// End of test
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [HttpPost]
        public ExecutedLog ExecutedLog([FromBody]ResultModel result)
        {
            ExecutedLog exec = new ExecutedLog
            {
                AngryPercent = result.Result,
                Name = result.Name
            };

            context.Add(exec);
            context.SaveChanges();
            return exec;
        }
    }
}