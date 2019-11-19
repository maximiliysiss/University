using Chemical.Forms.Controls.Models.Model;
using Chemical.Models;
using Chemical.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemical.Forms.Controls.Models.List
{
    public class MaterialInStockList : BaseModelListControl
    {
        public int ID { get; set; }
        public MaterialInStockList(int id)
        {
            this.ID = id;
        }

        protected override void AddNew() => Open(new MaterialInStock { StockId = ID });

        protected override List<object> Load()
        {
            DatabaseContext databaseContext = App.ChemicalModules.Resolve<DatabaseContext>();
            return databaseContext.MaterialInStocks.Include(x => x.Material).Include(x => x.Stock).Where(x => x.Stock.ID == ID).Cast<object>().ToList();
        }

        protected override void Open(object obj) => new MaterialOnStockControl(obj as MaterialInStock).ShowDialog();
    }
}
