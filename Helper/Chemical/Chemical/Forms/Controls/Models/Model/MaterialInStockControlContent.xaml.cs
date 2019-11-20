using Chemical.Forms.Controls.Models.List;
using Chemical.Models;
using Chemical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chemical.Forms.Controls.Models.Model
{
    public class MaterialOnStockControl : BaseModelControl<MaterialInStock>
    {
        public MaterialOnStockControl(MaterialInStock obj) : base(obj, new MaterialInStockControlContent(obj))
        {
            Title = "Сырье на складе";
        }

        public override bool IsEdit(MaterialInStock obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for MaterialInStockControlContent.xaml
    /// </summary>
    public partial class MaterialInStockControlContent : UserControl
    {
        public MaterialInStockControlContent(MaterialInStock materialInStock)
        {
            InitializeComponent();
            this.RawMaterials.ItemsSource = App.ChemicalModules.Resolve<DatabaseContext>().RawMaterials.ToList();
            this.DataContext = materialInStock;
        }
    }
}
