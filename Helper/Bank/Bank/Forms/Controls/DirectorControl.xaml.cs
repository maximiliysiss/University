using Bank.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Bank.Forms.Controls
{
    /// <summary>
    /// Interaction logic for DirectorControl.xaml
    /// </summary>
    public partial class DirectorControl : UserControl
    {
        public DirectorControl()
        {
            InitializeComponent();
            this.Workers.Content = new DirectorUserList();
            this.Accounts.Content = new PrivateAccountList();
            this.Currency.Content = new CurrenciesList();
            this.Convert.Content = new ConvertList();
        }
    }
}
