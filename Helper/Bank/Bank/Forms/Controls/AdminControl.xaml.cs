using Bank.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Bank.Forms.Controls
{
    /// <summary>
    /// Interaction logic for AdminControl.xaml
    /// </summary>
    public partial class AdminControl : UserControl
    {
        public AdminControl()
        {
            InitializeComponent();
            this.Users.Content = new UsersList();
        }
    }
}
