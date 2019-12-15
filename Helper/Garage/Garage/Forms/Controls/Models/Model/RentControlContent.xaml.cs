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

            var lastAction = db.Actions.OrderByDescending(x => x.DateTime).FirstOrDefault();
            if ((lastAction?.ActionType ?? ActionType.In) == ActionType.In)
            {
                Action.Content = "Выехать";
                Action.Click += Action_Out_Click;
            }
            else
            {
                Action.Content = "Въехать";
                Action.Click += Action_In_Click;
            }

            if (obj.EndDate != null)
                MainGrid.RowDefinitions.RemoveAt(0);
        }

        private void Action_In_Click(object sender, RoutedEventArgs e)
        {
            var action = new Garage.Models.Action { ActionType = ActionType.In, RentId = (this.DataContext as Rent).ID };
            db.Add(action);
            db.SaveChanges();
            Close();
        }

        private void Action_Out_Click(object sender, RoutedEventArgs e)
        {
            var action = new Garage.Models.Action { ActionType = ActionType.Out, RentId = (this.DataContext as Rent).ID };
            db.Add(action);
            db.SaveChanges();
            Close();
        }

        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            var rent = this.DataContext as Rent;
            rent.BoxId = rent.Box.ID;
            rent.Box = null;
            db.Add(rent);
            db.SaveChanges();
            Close();
        }

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
