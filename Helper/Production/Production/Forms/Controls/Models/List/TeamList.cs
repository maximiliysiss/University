using Production.Forms.Controls.Models.Model;
using Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Forms.Controls.Models.List
{
    public class TeamList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Team());

        protected override List<object> Load() => App.Db.Teams.Cast<object>().ToList();

        protected override void Open(object obj) => new TeamControl(obj as Team).ShowDialog();
    }
}
