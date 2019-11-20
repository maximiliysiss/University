using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemical.Models
{
    /// <summary>
    /// Роли
    /// </summary>
    public enum UserRole
    {
        Storage,
        ProWorker,
        Techolog
    }

    /// <summary>
    /// Пользователь
    /// </summary>
    [Serializable]
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
    }
}
