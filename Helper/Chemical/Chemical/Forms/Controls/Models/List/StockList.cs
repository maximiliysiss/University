using Chemical.Forms.Controls.Models.Model;
using Chemical.Models;
using Chemical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemical.Forms.Controls.Models.List
{
    public class StockList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Stock());

        protected override List<object> Load()
        {
            DatabaseContext databaseContext = App.ChemicalModules.Resolve<DatabaseContext>();
            return databaseContext.Stocks.Cast<object>().ToList();
        }

        protected override void Open(object obj) => new StockControl(obj as Stock).ShowDialog();
    }
}
