using Chemical.Models;
using System;
using System.Linq;

namespace Chemical.Services
{
    public interface ILoginService
    {
        User LoginAttempt(string login, string password);
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
                return databaseContext.Users.FirstOrDefault(x => x.Login == login && x.PasswordHash == CryptService.CreateMD5(password));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
