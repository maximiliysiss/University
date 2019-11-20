using Chemical.Models;
using System.Windows;
using System.Windows.Controls;

namespace Chemical.Forms.Controls.Models.Model
{
    public class RawMaterialControl : BaseModelControl<RawMaterial>
    {
        public RawMaterialControl(RawMaterial obj) : base(obj, new RawMaterialControlContent(obj))
        {
            Title = "Сырье";
        }

        public override bool IsEdit(RawMaterial obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for RawMaterialControl.xaml
    /// </summary>
    public partial class RawMaterialControlContent : UserControl
    {
        public RawMaterialControlContent(RawMaterial rawMaterial)
        {
            InitializeComponent();
            this.DataContext = rawMaterial;
        }
    }
}
