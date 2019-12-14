using Microsoft.EntityFrameworkCore;
using Production.Forms.Controls.Models.Model;
using Production.Models;
using Production.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Forms.Controls.Models.List
{
    public class UserList : BaseModelListControl
    {
        protected override void AddNew() => Open(new User());

        protected override List<object> Load() => App.Db.Users.Include(x => x.Team).Cast<object>().ToList();

        protected override void Open(object obj) => new UsersControl(obj as User).ShowDialog();
    }
}
