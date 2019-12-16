using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flats.Models.Controllers
{
    /// <summary>
    /// Модель для входа и регистрации
    /// </summary>
    public class LoginRegisterModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
