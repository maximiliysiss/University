using Chemical.Models;
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
    public class DetailMaterialControl : BaseModelControl<DetailMaterial>
    {
        public DetailMaterialControl(DetailMaterial obj) : base(obj, new DetailMaterialControlContent(obj))
        {
        }

        public override bool IsEdit(DetailMaterial obj) => obj.ID != 0;

        protected override void PrevAction(DetailMaterial obj)
        {
            base.PrevAction(obj);
            obj.Detail = null;
        }
    }

    /// <summary>
    /// Interaction logic for DetailMaterialControlContent.xaml
    /// </summary>
    public partial class DetailMaterialControlContent : UserControl
    {
        public DetailMaterialControlContent(DetailMaterial obj)
        {
            InitializeComponent();
            this.DataContext = obj;
            this.Materials.ItemsSource = App.Db.RawMaterials.ToList();
        }
    }
}
