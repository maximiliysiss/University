using Chemical.Forms.Controls.Models.Model;
using Chemical.Models;
using Chemical.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemical.Forms.Controls.Models.List
{
    /// <summary>
    /// Форма списка планов
    /// </summary>
    public class PlanList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Plan());

        protected override List<object> Load()
        {
            DatabaseContext databaseContext = App.ChemicalModules.Resolve<DatabaseContext>();
            return databaseContext.Plans.Include(x => x.Material).Include(x => x.Stock).Cast<object>().ToList();
        }

        protected override void Open(object obj) => new PlanControl(obj as Plan).ShowDialog();
    }
}
