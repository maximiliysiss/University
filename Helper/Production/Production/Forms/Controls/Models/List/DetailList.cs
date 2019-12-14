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
    public class DetailList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Detail());

        protected override List<object> Load() => App.Db.Details.Cast<object>().ToList();

        protected override void Open(object obj) => new DetailControl(obj as Detail).ShowDialog();
    }
}
