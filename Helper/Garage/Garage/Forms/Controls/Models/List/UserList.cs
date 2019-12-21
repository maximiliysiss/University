using Garage.Models;
using System.Collections.Generic;
using System.Linq;

namespace Garage.Forms.Controls.Models.List
{
    public class UserList : BaseModelListControl
    {
        public UserList()
        {
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void AddNew() { }

        protected override List<object> Load() => App.Db.Users.Where(x => x.UserRole == Garage.Models.UserRole.User).Cast<object>().ToList();

        protected override void Open(object obj) => new Model.UsersControl(obj as User).ShowDialog();
    }
}
