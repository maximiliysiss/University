using Bank.Forms;
using System.Windows;
using System.Windows.Controls;

namespace Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(UserControl userControl)
        {
            InitializeComponent();
            // При закрытии открыть Login
            this.Closing += (s, e) => new Login().Show();
            this.Content = userControl;
        }
    }
}
