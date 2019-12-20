using System.Windows;
using System.Windows.Controls;

namespace Childhood.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(UserControl control)
        {
            InitializeComponent();
            this.Content = control;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) => new Login().Show();
    }
}
