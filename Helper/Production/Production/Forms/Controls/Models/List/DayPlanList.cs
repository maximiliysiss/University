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
    /// План на день (список)
    /// </summary>
    public class DayPlanList : BaseModelListControl
    {
        /// <summary>
        /// Добавить
        /// </summary>
        protected override void AddNew() => Open(new DayPlan());

        /// <summary>
        /// Загрзуить список из бд
        /// </summary>
        /// <returns></returns>
        protected override List<object> Load() => App.Db.DayPlans.Include(x => x.Schedule)
            .ThenInclude(x=>x.Team).Include(x => x.Detail).Cast<object>().ToList();

        /// <summary>
        /// Отркыть
        /// </summary>
        /// <param name="obj"></param>
        protected override void Open(object obj) => new DayPlanControl(obj as DayPlan).ShowDialog();
    }
}
