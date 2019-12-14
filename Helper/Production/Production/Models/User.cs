﻿using Production.Extensions.Attributes;
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
        public User()
        {
        }

        public User(User user)
        {
            this.Login = user.Login;
        }

        [HideColumnIfAutoGenerated]
        public int ID { get; set; }
        [DisplayGridName("Логин")]
        public string Login { get; set; }
        [HideColumnIfAutoGenerated]
        public string PasswordHash { get; set; }
        [DisplayGridName("Роль")]
        public UserRole UserRole { get; set; }
        [HideColumnIfAutoGenerated]
        public int? TeamId { get; set; }
        [DisplayGridName("Бригада")]
        public Team Team { get; set; }

        public override string ToString() => Login;
    }
}
