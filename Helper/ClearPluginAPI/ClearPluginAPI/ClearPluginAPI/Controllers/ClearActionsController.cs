using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClearPluginAPI.Models;
using ClearPluginAPI.Services;

namespace ClearPluginAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClearActionsController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly DatabaseContext _context;

        public ClearActionsController(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все очистки
        /// </summary>
        /// <returns></returns>
        // GET: api/ClearActions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClearAction>>> GetClearActions()
        {
            return await _context.ClearActions.ToListAsync();
        }

        /// <summary>
        /// Добавить очистку
        /// </summary>
        /// <param name="clearAction"></param>
        /// <returns></returns>
        // POST: api/ClearActions
        [HttpPost]
        public async Task<ActionResult<ClearAction>> PostClearAction(ClearAction clearAction)
        {
            clearAction.IP = this.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            clearAction.Name = this.Request.HttpContext.Connection.Id;
            _context.ClearActions.Add(clearAction);
            await _context.SaveChangesAsync();

            return clearAction;
        }
    }
}
