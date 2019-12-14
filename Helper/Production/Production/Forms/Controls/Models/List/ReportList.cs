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
    public class ReportList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Report());

        protected override List<object> Load() => App.Db.Reports.Include(x=>x.User).Include(x => x.DayPlan).ThenInclude(x => x.Detail)
            .Include(x => x.DayPlan).ThenInclude(x => x.Schedule).ThenInclude(x => x.Team).Where(x => x.UserId == App.user.ID).Cast<object>().ToList();

        protected override void Open(object obj) => new ReportControl(obj as Report).ShowDialog();
    }

    public class BrigadirReportList : ReportList
    {
        public BrigadirReportList()
        {
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override List<object> Load() => App.Db.Reports.Include(x => x.User).Include(x => x.DayPlan).ThenInclude(x => x.Detail)
            .Include(x => x.DayPlan).ThenInclude(x => x.Schedule).ThenInclude(x => x.Team)
            .Where(x => x.DayPlan.Schedule.TeamId == App.user.TeamId).Cast<object>().ToList();

        protected override void Open(object obj) { }
    }
}
