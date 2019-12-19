using Chemical.Forms.Controls.Models.List;
using Chemical.Models;
using System.Windows.Controls;

namespace Chemical.Forms.Controls.Models.Model
{
    public class DetailControl : BaseModelControl<Detail>
    {
        public DetailControl(Detail obj) : base(obj, new DetailControlContent(obj))
        {
        }

        public override bool IsEdit(Detail obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for DetailControlContent.xaml
    /// </summary>
    public partial class DetailControlContent : UserControl
    {
        public DetailControlContent(Detail obj, DetailMaterialList detailMaterial = null)
        {
            InitializeComponent();
            this.DataContext = obj;
            this.Materials.Children.Add(detailMaterial ?? new DetailMaterialList(obj));
        }
    }
}
