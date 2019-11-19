using Chemical.Forms.Controls.Models.List;
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

namespace Chemical.Forms.Controls
{
    /// <summary>
    /// Interaction logic for Technolog.xaml
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
