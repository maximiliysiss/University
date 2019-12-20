using System;
using System.Collections.Generic;
using System.Text;

namespace Childhood.Models
{
    public enum UserType
    {
        Director,
        Tutor,
        Parent
    }

    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
    }
}
