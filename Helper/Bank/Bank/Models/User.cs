using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Models
{
    public enum Role
    {
        Admin,
        Client,
        Director,
        Worker
    }

    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public string DocumentCode { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
