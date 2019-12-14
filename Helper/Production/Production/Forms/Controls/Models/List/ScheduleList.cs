using Microsoft.EntityFrameworkCore;
using Production.Forms.Controls.Models.Model;
using Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Forms.Controls.Models.List
{
    public class ScheduleList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Schedule());

        protected override List<object> Load() => App.Db.Schedules.Include(x => x.Team).Cast<object>().ToList();

        protected override void Open(object obj) => new ScheduleControl(obj as Schedule).ShowDialog();
    }
}
