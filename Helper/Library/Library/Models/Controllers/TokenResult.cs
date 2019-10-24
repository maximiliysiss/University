using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Controllers
{
    public class TokenResult
    {
        public UserRole UserRole { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
