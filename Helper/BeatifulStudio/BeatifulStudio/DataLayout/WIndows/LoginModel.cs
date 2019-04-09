using BeatifulStudio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatifulStudio.DataLayout.WIndows
{
    public class LoginModel
    {

        public string Login { get; set; }
        private string password;

        public LoginModel(string login, string password)
        {
            Login = login;
            this.password = password;
        }

        public LoginModel()
        {
        }

        public string Password
        {
            get => password;
            set => password = CryptService.CreateMD5(value);
        }
    }
}
