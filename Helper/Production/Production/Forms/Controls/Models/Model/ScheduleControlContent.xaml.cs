using Production.Models;
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

namespace Production.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма расписания
    /// </summary>
    public class ScheduleControl : BaseModelControl<Schedule>
    {
        public ScheduleControl(Schedule obj) : base(obj, new ScheduleControlContent(obj))
        {
        }

        public override bool IsEdit(Schedule obj) => obj.ID != 0;
    }


    /// <summary>
    /// Interaction logic for ScheduleControlContent.xaml
    /// </summary>
    public partial class ScheduleControlContent : UserControl
    {
        public ScheduleControlContent(Schedule schedule)
        {
            InitializeComponent();
            this.Teams.ItemsSource = App.Db.Teams.ToList();
            this.DataContext = schedule;
        }
    }
}
