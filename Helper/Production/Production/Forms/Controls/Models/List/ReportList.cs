using Production.Forms.Controls.Models.Model;
using Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Forms.Controls.Models.List
{
    public class ReportList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Report());

        protected override List<object> Load() => App.Db.Reports.Cast<object>().ToList();

        protected override void Open(object obj) => new ReportControl(obj as Report).ShowDialog();
    }
}
