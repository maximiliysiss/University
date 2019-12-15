using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage.Forms.Controls.Models.List
{
    public class ActionList : BaseModelListControl
    {
        public ActionList()
        {
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void AddNew() { }

        protected override List<object> Load() => App.Db.Actions.Cast<object>().ToList();

        protected override void Open(object obj) { }
    }
}
