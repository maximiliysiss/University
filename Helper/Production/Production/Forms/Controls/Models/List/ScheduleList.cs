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
    public class ScheduleList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Schedule());

        protected override List<object> Load() => App.ProductionModule.Resolve<DatabaseContext>().Schedules.Where(x => !x.Executed).Cast<object>().ToList();

        protected override void Open(object obj) => new ScheduleControl(obj as Schedule).ShowDialog();
    }

    public class ArchiveScheduleList : BaseModelListControl
    {
        public ArchiveScheduleList()
        {
            this.Add.Visibility = System.Windows.Visibility.Hidden;
        }

        protected override void AddNew() { }

        protected override List<object> Load() => App.ProductionModule.Resolve<DatabaseContext>().Schedules.Where(x => x.Executed).Cast<object>().ToList();

        protected override void Open(object obj) => new ViewScheduleControl(obj as Schedule).ShowDialog();
    }
}
