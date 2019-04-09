using BeatifulStudio.DataLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeatifulStudio.Services
{
    public class FormConstructor
    {
        public static List<DateTime> GetDays(int day, int month, int year) => DateTimeServices.AllDatesInMonth(year, month)
                        .Where(x => x.DayOfWeek == (DayOfWeek)day).ToList();

        public static List<DateTime> UsersDateTimes(DateTime dateTime, User user)
        {
            return user.Schedule.Select((d, i) => new { d, i })
                            .Where(x => x.d.ToString() == "0").Select(x => GetDays(x.i, dateTime.Month, dateTime.Year))
                            .SelectMany(x => x).ToList();
        }

        public static void SetWorkDay(Calendar calendar, User user)
        {
            calendar.BlackoutDates.Clear();
            foreach (var dateTime in UsersDateTimes(calendar.DisplayDate, user))
                calendar.BlackoutDates.Add(new CalendarDateRange { End = dateTime, Start = dateTime });
            calendar.DisplayDateChanged -= Calendar_DisplayDateChanged;
            calendar.DisplayDateChanged += Calendar_DisplayDateChanged;
        }

        private static void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            SetWorkDay(sender as Calendar, (sender as Calendar).DataContext as User);
        }

        public static Grid GenerateLine(User user)
        {
            Grid grid = new Grid { Height = 200 };
            grid.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(30) });
            grid.RowDefinitions.Add(new RowDefinition());

            grid.Children.Add(new Label
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                FontSize = 16,
                Content = user.Login
            });

            var calendar = new Calendar { Name = $"Calendar{user.ID}", DataContext = user };
            SetWorkDay(calendar, user);

            Viewbox viewbox = new Viewbox
            {
                Child = calendar
            };
            viewbox.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(viewbox);
            return grid;
        }
    }
}
