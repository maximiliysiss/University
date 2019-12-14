using Production.Forms.Controls.Models.List;
using Production.Forms.Controls.Models.Model;
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
using System.Windows.Shapes;

namespace Production.Forms.Controls
{
    /// <summary>
    /// Interaction logic for BrigadirControl.xaml
    /// </summary>
    public partial class BrigadirControl : UserControl
    {
        public BrigadirControl()
        {
            InitializeComponent();
            this.Teams.Content = new UserTeamList(App.user);
            this.DayPlans.Content = new DayPlanList();
        }
    }
}
