using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_angry_service.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; }
    }
}
