using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateChatter.Models
{
    public class User : IdentityUser
    {
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
