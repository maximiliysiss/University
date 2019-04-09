using BeatifulStudio.DataLayout;
using BeatifulStudio.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeatifulStudio.Forms
{
    /// <summary>
    /// Interaction logic for MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        public DatabaseContext DatabaseContext { get; set; } = new DatabaseContext();
        public List<dynamic> ClientsList { get; set; }

        public List<DateTime> GetDays(int day, int month, int year) => DateTimeServices.AllDatesInMonth(year, month)
            .Where(x => x.DayOfWeek == (DayOfWeek)day).ToList();

        public List<DateTime> UsersDateTimes(DateTime dateTime)
        {
            return App.User.Schedule.Select((d, i) => new { d, i })
                            .Where(x => x.d.ToString() == "0").Select(x => GetDays(x.i, dateTime.Month, dateTime.Year))
                            .SelectMany(x => x).ToList();
        }

        public MasterPage()
        {
            InitializeComponent();
            ClientsList = DatabaseContext.UsersServices.Where(x => x.Master.ID == App.User.ID).Include(x => x.Service)
                .Include(x => x.User).Select(x => new { x.Service.Name, x.User.Login, x.DateTime }).Cast<dynamic>().ToList();
            ReloadCalendar();
            Clients.ItemsSource = ClientsList;
        }

        private void ReloadCalendar()
        {
            Calendar.BlackoutDates.Clear();
            foreach (var service in ClientsList)
                Calendar.BlackoutDates.Add(new CalendarDateRange { Start = service.DateTime, End = service.DateTime });
            foreach (var dateTime in UsersDateTimes(Calendar.DisplayDate))
                Calendar.BlackoutDates.Add(new CalendarDateRange { End = dateTime, Start = dateTime });
            Calendar.DisplayDateChanged -= Calendar_DisplayDateChanged;
            Calendar.DisplayDateChanged += Calendar_DisplayDateChanged;
        }

        private void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            ReloadCalendar();
        }

        private void CalendarMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CalendarDayButton button = sender as CalendarDayButton;
            DateTime clickedDate = (DateTime)button.DataContext;
            var find = ClientsList.Select((x, i) => new { x, i }).FirstOrDefault(x => x.x.DateTime == clickedDate);
            if (find != null)
                Clients.SelectedIndex = find.i;
            else
                Clients.SelectedIndex = -1;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginService.RemoveLogin();
            NavigationService.GoBack();
        }
    }
}
