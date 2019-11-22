using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    public enum UserRole
    {
        Admin,
        Director,
        Brigadir,
        Worker
    }

    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
    }

    public class Worker : User
    {
        public Team Team { get; set; }
    }
}
