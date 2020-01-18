using Bank.Forms.Controls.Models.Model;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Forms.Controls.Models.List
{
    /// <summary>
    /// Список пользователей
    /// </summary>
    public class UsersList : BaseModelListControl
    {
        protected override void AddNew() => Open(new User());

        protected override List<object> Load() => App.Db.Users.Cast<object>().ToList();

        protected override void Open(object obj) => new UsersControl(obj as User).ShowDialog();
    }

    /// <summary>
    /// Список работников для директора
    /// </summary>
    public class DirectorUserList : BaseModelListControl
    {
        protected override void AddNew() => Open(new User { Role = Role.Worker });

        protected override List<object> Load() => App.Db.Users.Where(x => x.Role == Role.Worker).Cast<object>().ToList();

        protected override void Open(object obj) => new DirectorUsersControl(obj as User).ShowDialog();
    }

    /// <summary>
    /// Список клиентов
    /// </summary>
    public class ClientList : DirectorUserList
    {
        protected override void AddNew() => Open(new User { Role = Role.Client });

        protected override List<object> Load() => App.Db.Users.Where(x => x.Role == Role.Client).Cast<object>().ToList();
    }
}
