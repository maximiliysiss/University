using Bank.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Bank.Forms.Controls
{
    /// <summary>
    /// Interaction logic for WorkerControl.xaml
    /// </summary>
    public partial class WorkerControl : UserControl
    {
        public WorkerControl()
        {
            InitializeComponent();
            this.Clients.Content = new ClientList();
            this.Accounts.Content = new ClientPrivateAccountList();
        }
    }
}
