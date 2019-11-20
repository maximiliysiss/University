using Chemical.Forms.Controls.Models.Model;
using Chemical.Models;
using Chemical.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chemical.Forms.Controls.Models.List
{
    public class RawMaterialList : BaseModelListControl
    {
        protected override void AddNew() => new RawMaterialControl(new Chemical.Models.RawMaterial()).ShowDialog();

        protected override List<object> Load()
        {
            DatabaseContext databaseContext = App.ChemicalModules.Resolve<DatabaseContext>();
            return databaseContext.RawMaterials.Include(x => x.MaterialInStocks).ThenInclude(x => x.Material)
                .Include(x => x.MaterialInStocks).ThenInclude(x => x.Stock).Cast<object>().ToList();
        }

        protected override void Open(object obj) => new RawMaterialControl(obj as RawMaterial).ShowDialog();
    }
}
