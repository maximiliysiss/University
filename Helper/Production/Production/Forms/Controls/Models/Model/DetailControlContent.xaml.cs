using Production.Models;
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

namespace Production.Forms.Controls.Models.Model
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
        public DetailControlContent(Detail detail)
        {
            InitializeComponent();
            this.DataContext = detail;
        }
    }
}
