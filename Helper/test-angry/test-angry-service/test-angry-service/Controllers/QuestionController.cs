using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using test_angry_service.Models;
using test_angry_service.Services;

namespace test_angry_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private DatabaseContext context;

        public QuestionController(DatabaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get questions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<string> Questions() => context.Questions.Select(x => x.Content).ToList();
    }
}