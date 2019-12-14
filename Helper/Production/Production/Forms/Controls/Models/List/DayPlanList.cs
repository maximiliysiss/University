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
    public class DayPlanList : BaseModelListControl
    {
        protected override void AddNew() => Open(new DayPlan());

        protected override List<object> Load() => App.Db.DayPlans.Include(x => x.Schedule)
            .ThenInclude(x=>x.Team).Include(x => x.Detail).Cast<object>().ToList();

        protected override void Open(object obj) => new DayPlanControl(obj as DayPlan).ShowDialog();
    }
}
