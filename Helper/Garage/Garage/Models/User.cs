using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    public enum UserRole
    {
        User,
        HomeKeeper
    }

    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public UserRole UserRole { get; set; } = UserRole.User;
    }
}
