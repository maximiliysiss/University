using Garage.Models;
using System;
using System.Linq;

namespace Garage.Services
{
    public interface ILoginService
    {
        User LoginAttempt(string login, string password);
        User Register(string login, string password);
    }

    public class LoginService : ILoginService
    {
        private readonly DatabaseContext databaseContext;

        public LoginService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public User LoginAttempt(string login, string password)
        {
            try
            {
                return App.user = databaseContext.Users.FirstOrDefault(x => x.Login == login && x.PasswordHash == CryptService.CreateMD5(password));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User Register(string login, string password)
        {
            try
            {
                var user = databaseContext.Users.FirstOrDefault(x => x.Login == login);
                if (user != null)
                    return null;
                user = new User { Login = login.Trim(), PasswordHash = CryptService.CreateMD5(password.Trim()) };
                databaseContext.Add(user);
                databaseContext.SaveChanges();
                return App.user = user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
