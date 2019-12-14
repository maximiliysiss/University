using Production.Forms.Controls.Models.List;
using Production.Models;
using System.Windows.Controls;

namespace Production.Forms.Controls
{
    /// <summary>
    /// Interaction logic for DirectorControl.xaml
    /// </summary>
    public partial class DirectorControl : UserControl
    {
        public DirectorControl()
        {
            InitializeComponent();
            Users.Content = new UserList();
            Details.Content = new DetailList();
            Teams.Content = new TeamList();
            Schedules.Content = new ScheduleList();
        }
    }
}
