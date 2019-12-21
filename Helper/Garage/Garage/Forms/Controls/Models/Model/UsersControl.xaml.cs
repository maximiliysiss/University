using Garage.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Garage.Forms.Controls.Models.Model
{
    /// <summary>
    /// Interaction logic for UsersControl.xaml
    /// </summary>
    public partial class UsersControl : Window
    {
        public UsersControl(User user)
        {
            InitializeComponent();
            var rents = App.Db.Rents.Where(x => x.UserId == user.ID).ToList();
            if (rents.Count > 0)
                this.Actions.ItemsSource = rents;
            else
            {
                MainGrid.Children.RemoveAt(MainGrid.Children.Count - 1);
                var textBlock = new TextBlock
                {
                    Text = "Данный пользователь не брал в аренду",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                textBlock.SetValue(Grid.RowProperty, 2);
                textBlock.SetValue(Grid.ColumnSpanProperty, 2);
                MainGrid.Children.Add(textBlock);
            }
            this.DataContext = user;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var db = App.Db;
            db.Entry(this.DataContext).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            Close();
        }
    }
}
