using Childhood.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Childhood.Models
{
    public enum UserType
    {
        [Description("Директор")]
        Director,
        [Description("Воспитатель")]
        Tutor,
        [Description("Родитель")]
        Parent
    }

    public class User
    {
        [HideColumn]
        public int ID { get; set; }
        [DisplayGridName("Логин")]
        public string Login { get; set; }
        [DisplayGridName("Хэш пароля")]
        public string PasswordHash { get; set; }
        [DisplayGridName("Тип")]
        public UserType UserType { get; set; }
        [DisplayGridName("ФИО")]
        public string FIO { get; set; }
        [DisplayGridName("Телефон")]
        public string Phone { get; set; }

        public override string ToString() => $"{FIO} : {Phone}";
    }
}
