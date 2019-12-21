using Childhood.Forms.Controls.Models.Model;
using Childhood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Childhood.Forms.Controls.Models.List
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
}
