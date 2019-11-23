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
    public class FailControl : BaseModelControl<FailDetail>
    {
        public FailControl(FailDetail obj) : base(obj, new FailControlContent(obj))
        {
        }

        public override bool IsEdit(FailDetail obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for FailControlContent.xaml
    /// </summary>
    public partial class FailControlContent : UserControl
    {
        public FailControlContent(FailDetail failDetail)
        {
            InitializeComponent();
            this.Details.ItemsSource = App.Db.Details.ToList();
            this.DataContext = failDetail;
        }
    }
}
