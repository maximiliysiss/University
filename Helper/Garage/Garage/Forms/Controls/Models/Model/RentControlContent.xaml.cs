using Garage.Models;
using Garage.Services;
using System;
using System.Linq;
using System.Windows;

namespace Garage.Forms.Controls.Models.Model
{
    /// <summary>
    /// Interaction logic for RentControlContent.xaml
    /// </summary>
    public partial class RentControl : Window
    {
        private readonly DatabaseContext db = App.Db;

        public RentControl(Rent obj)
        {
            InitializeComponent();
            this.DataContext = obj;
            this.Rent.Visibility = obj.ID == 0 ? Visibility.Visible : Visibility.Collapsed;
            this.EndRent.Visibility = this.Action.Visibility = obj.ID != 0 && obj.EndDate == null ? Visibility.Visible : Visibility.Collapsed;

            // получим последнее действие
            var lastAction = db.Actions.OrderByDescending(x => x.DateTime).FirstOrDefault();
            // если нету или In
            if ((lastAction?.ActionType ?? ActionType.In) == ActionType.In)
            {
                Action.Content = "Выехать";
                Action.Click += (s, e) => HandlerAction(ActionType.Out);
            }
            else
            {
                Action.Content = "Въехать";
                Action.Click += (s, e) => HandlerAction(ActionType.In);
            }

            // Если история, то скрываем действия
            if (obj.EndDate != null)
                MainGrid.RowDefinitions.RemoveAt(0);
        }

        /// <summary>
        /// Действие над действием
        /// </summary>
        /// <param name="actionType"></param>
        private void HandlerAction(ActionType actionType)
        {
            var action = new Garage.Models.Action { ActionType = actionType, RentId = (this.DataContext as Rent).ID };
            db.Add(action);
            db.SaveChanges();
            Close();
        }

        /// <summary>
        /// Арендовать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            var rent = this.DataContext as Rent;
            rent.BoxId = rent.Box.ID;
            rent.Box = null;
            db.Add(rent);
            db.SaveChanges();
            Close();
        }

        /// <summary>
        /// Отказ от аренды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndRent_Click(object sender, RoutedEventArgs e)
        {
            var rent = this.DataContext as Rent;
            rent.EndDate = DateTime.Now;
            db.Update(rent);
            db.SaveChanges();
            Close();
        }
    }
}
