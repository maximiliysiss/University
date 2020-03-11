using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test_angry_service.Models.Controllers
{
    public class RegisterModel
    {
        [Required]
        [MinLength(4)]
        public string Nickname { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
