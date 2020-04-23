using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Models.Controller
{
    /// <summary>
    /// Сменить пароль
    /// </summary>
    public class PasswordRestore
    {
        [Required]
        public int UserId { get; set; }
        [MinLength(6)]
        [Required]
        public string Password { get; set; }
        [MinLength(6)]
        [Required]
        public string PrevPassword { get; set; }
    }
}
