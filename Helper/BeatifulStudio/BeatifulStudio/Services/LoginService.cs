using BeatifulStudio.DataLayout.WIndows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatifulStudio.Services
{
    public class LoginService
    {
        public static LoginModel TryGetLoginInfo()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BeatifulStudio", true);
            if (key == null)
            {
                var newKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\BeatifulStudio");
                newKey.Close();
                return null;
            }

            var login = key.GetValue("Login")?.ToString();
            var password = key.GetValue("Password")?.ToString();
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;
            return new LoginModel(login, password);
        }

        public static void RemoveLogin()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BeatifulStudio");
            if (key == null)
                return;
            key.Close();
            Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\BeatifulStudio");
        }

        public static void TryAddLogin(LoginModel loginModel)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BeatifulStudio", true);
            if (key == null)
                key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\BeatifulStudio", true);
            key.SetValue("Login", loginModel.Login);
            key.SetValue("Password", loginModel.Password);
            key.Close();
        }
    }
}
