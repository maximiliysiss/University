using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Models.Controller
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
