using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolService.Models.Controllers
{
    public class AuthToken
    {

        private string token;
        public string Token
        {
            get => $"Bearer {token}";
            set => token = value;
        }

        public UserType UserType { get; set; }
    }
}
