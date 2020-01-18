using Bank.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Models
{
    /// <summary>
    /// Роль
    /// </summary>
    public enum Role
    {
        Admin,
        Client,
        Director,
        Worker
    }

    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        [HideColumn]
        public int Id { get; set; }
        [DisplayGridName("Роль")]
        public Role Role { get; set; }
        [DisplayGridName("ФИО")]
        public string FIO { get; set; }
        [DisplayGridName("Адрес")]
        public string Address { get; set; }
        [DisplayGridName("Код документа")]
        public string DocumentCode { get; set; }
        [DisplayGridName("Логин")]
        public string Login { get; set; }
        [HideColumn]
        public string PasswordHash { get; set; }

        public override string ToString() => $"{FIO}|{Address}";
    }
}
