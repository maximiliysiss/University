using Garage.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Garage.Forms.Controls
{
    /// <summary>
    /// Interaction logic for HomeKeeperControl.xaml
    /// </summary>
    public partial class HomeKeeperControl : UserControl
    {
        public HomeKeeperControl()
        {
            InitializeComponent();
            this.Boxes.Content = new BoxesList();
            this.Users.Content = new UserList();
            this.Actions.Content = new ActionList();
        }
    }
}
