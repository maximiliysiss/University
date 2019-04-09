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

namespace BeatifulStudio.Forms.Service
{
    /// <summary>
    /// Interaction logic for SelectDate.xaml
    /// </summary>
    public partial class SelectDate : Window
    {
        public DateTime? DateTime { get; set; }
        public SelectDate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime = Calendar.SelectedDate;
            this.Close();
        }
    }
}
