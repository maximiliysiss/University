using Production.Models;
using Production.Services;
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

namespace Production.Forms.Controls.Models.Model
{
    /// <summary>
    /// Interaction logic for ViewScheduleControl.xaml
    /// </summary>
    public partial class ViewScheduleControl : Window
    {
        public ViewScheduleControl(Schedule schedule)
        {
            var db = App.ProductionModule.Resolve<DatabaseContext>();
            InitializeComponent();
            this.Details.ItemsSource = db.Details.ToList();
            this.DataContext = schedule;
        }
    }
}
