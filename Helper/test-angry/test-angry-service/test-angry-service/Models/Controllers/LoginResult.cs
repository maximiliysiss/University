using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_angry_service.Models.Controllers
{
    public class LoginResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Name { get; set; }
    }
}
