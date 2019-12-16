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
    /// <summary>
    /// Отчеты
    /// </summary>
    public class ReportList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Report());

        protected override List<object> Load() => App.Db.Reports.Include(x=>x.User).Include(x => x.DayPlan).ThenInclude(x => x.Detail)
            .Include(x => x.DayPlan).ThenInclude(x => x.Schedule).ThenInclude(x => x.Team).Where(x => x.UserId == App.user.ID).Cast<object>().ToList();

        protected override void Open(object obj) => new ReportControl(obj as Report).ShowDialog();
    }

    /// <summary>
    /// Отчеты для бригадира (нельзя изменять)
    /// </summary>
    public class BrigadirReportList : ReportList
    {
        /// <summary>
        /// Скроем кнопку добавить
        /// </summary>
        public BrigadirReportList()
        {
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override List<object> Load() => App.Db.Reports.Include(x => x.User).Include(x => x.DayPlan).ThenInclude(x => x.Detail)
            .Include(x => x.DayPlan).ThenInclude(x => x.Schedule).ThenInclude(x => x.Team)
            .Where(x => x.DayPlan.Schedule.TeamId == App.user.TeamId).Cast<object>().ToList();

        /// <summary>
        /// Ничего не открываем
        /// </summary>
        /// <param name="obj"></param>
        protected override void Open(object obj) { }
    }
}
