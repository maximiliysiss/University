using BeatifulStudio.DataLayout;
using BeatifulStudio.Services;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace BeatifulStudio.Forms
{
    /// <summary>
    /// Interaction logic for AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Page
    {
        public DatabaseContext databaseContext = new DatabaseContext();

        private void ReloadClients()
        {
            ClientsCalendar.BlackoutDates.Clear();
            var clients = databaseContext.UsersServices.Where(x => x.DateTime.Month == ClientsCalendar.DisplayDate.Month)
                .Include(x => x.Master).Include(x => x.Service).Include(x => x.User)
                .Select(x => new { x.DateTime, Master = x.Master.Login, Service = x.Service.Name, User = x.User.Login }).ToList();
            Clients.ItemsSource = clients;
            foreach (var date in clients.GroupBy(x => x.DateTime).Select(x => x.Key))
                ClientsCalendar.BlackoutDates.Add(new CalendarDateRange { End = date, Start = date });
        }

        public AdminForm()
        {
            InitializeComponent();
            var list = databaseContext.Users.Where(x => x.Role == DataLayout.Models.Role.Master).ToList();
            foreach (var user in list)
                SchedulesList.Children.Add(FormConstructor.GenerateLine(user));
            ReloadClients();
            ClientsCalendar.DisplayDateChanged += ClientsCalendar_DisplayDateChanged;
        }

        private void ClientsCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            ReloadClients();
        }

        private void FullReport(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                var name = saveFileDialog.FileName;
                new Thread(() =>
                {
                    var dataList = databaseContext.UsersServices.Include(x => x.User)
                    .Include(x => x.Master).Include(x => x.Service).Select(x => new
                    {
                        x.DateTime,
                        Master = x.Master.Login,
                        Service = x.Service.Name,
                        User = x.User.Login,
                        Price = x.Service.Price
                    }).ToList();
                    var wb = new XLWorkbook();
                    var ws = wb.Worksheets.Add("Full Report");
                    ws.Cell(1, 1).InsertData(dataList);
                    ws.Cell(1 + dataList.Count, 1).InsertData(new[] { new { Sum = dataList.Sum(x => x.Price) } });
                    wb.SaveAs(name);
                    wb.Dispose();
                    MessageBox.Show("Success report", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }).Start();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginService.RemoveLogin();
            NavigationService.GoBack();
        }
    }
}
