using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Forms.Controls.Models.List
{
    public class MakedDetailList : BaseModelListControl
    {
        public MakedDetailList()
        {
            this.Add.Visibility = System.Windows.Visibility.Hidden;
        }

        protected override void AddNew() { }

        protected override List<object> Load() => App.Db.MakedDetails.Cast<object>().ToList();

        protected override void Open(object obj) { }
    }
}
