using Production.Forms;
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

namespace Production
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserControl control;

        /// <summary>
        /// Старт окна
        /// </summary>
        /// <param name="control"></param>
        public MainWindow(UserControl control)
        {
            this.control = control;
            InitializeComponent();
            this.Content = this.control;

            this.Closing += MainWindow_Closing;
        }

        /// <summary>
        /// При выходе открыть форму входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new Login().Show();
        }
    }
}
