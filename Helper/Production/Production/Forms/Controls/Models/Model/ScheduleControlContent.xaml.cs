using Production.Models;
using Production.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Production.Forms.Controls.Models.Model
{
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
            var db = App.ProductionModule.Resolve<DatabaseContext>();
            InitializeComponent();
            this.Details.ItemsSource = db.Details.ToList();
            this.DataContext = schedule;
            if (schedule.ID != 0)
                this.offer.Visibility = db.Details.Find(schedule.ID).Count >= schedule.Count ? Visibility.Visible : Visibility.Hidden;
        }

        private void Offer_Click(object sender, RoutedEventArgs e)
        {
            var db = App.Db;
            var detail = Details.SelectedItem as Detail;
            var schedule = this.DataContext as Schedule;
            if (detail.Count >= schedule.Count)
            {
                detail.Count -= schedule.Count;
                schedule.Executed = true;
                db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.Entry(schedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                Window.GetWindow(this).Close();
            }
            else
                MessageBox.Show("Количество не соответствует доступному", "Ошибка");
        }
    }
}
