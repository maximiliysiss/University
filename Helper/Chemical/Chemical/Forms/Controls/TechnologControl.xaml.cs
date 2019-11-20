using Chemical.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Chemical.Forms.Controls
{
    /// <summary>
    /// Форма для технолога
    /// </summary>
    public partial class TechnologControl : UserControl
    {
        public TechnologControl()
        {
            InitializeComponent();
            RawMaterial.Content = new RawMaterialList();
            Plan.Content = new PlanList();
        }
    }
}
