using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage.Forms.Controls.Models.List
{
    /// <summary>
    /// Список действий
    /// </summary>
    public class ActionList : BaseModelListControl
    {
        /// <summary>
        /// Нельзя добавлять
        /// </summary>
        public ActionList()
        {
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void AddNew() { }

        protected override List<object> Load() => App.Db.Actions.Cast<object>().ToList();

        protected override void Open(object obj) { }
    }
}
