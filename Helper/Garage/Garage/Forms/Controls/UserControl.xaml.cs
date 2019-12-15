using Garage.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Garage.Forms.Controls
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class UsersControl : UserControl
    {
        public UsersControl()
        {
            InitializeComponent();
            this.Boxes.Content = new UserBoxesList();
            this.Rent.Content = new UserOwnBoxesList();
        }
    }
}
