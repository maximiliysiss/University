using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolService.Models.Controllers
{
    public class AuthToken
    {
        public int Id { get; set; }
        private string accessToken;
        public string AccessToken
        {
            get => $"Bearer {accessToken}";
            set => accessToken = value;
        }

        private string refreshToken;
        public string RefreshToken
        {
            get => $"Bearer {refreshToken}";
            set => refreshToken = value;
        }

        public UserType UserType { get; set; }
    }
}
