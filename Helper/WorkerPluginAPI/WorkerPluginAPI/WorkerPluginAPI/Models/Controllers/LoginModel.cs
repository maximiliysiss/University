using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Models.Controllers
{
    /// <summary>
    /// Модель для входа
    /// </summary>
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
