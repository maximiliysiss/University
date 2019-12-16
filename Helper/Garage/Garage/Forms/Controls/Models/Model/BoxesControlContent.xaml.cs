using Garage.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Garage.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма бокса
    /// </summary>
    public class BoxesControl : BaseModelControl<Box>
    {
        public BoxesControl(Box obj) : base(obj, new BoxesControlContent(obj))
        {
        }

        public override bool IsEdit(Box obj) => obj.ID != 0;
    }


    /// <summary>
    /// Interaction logic for BoxesControlContent.xaml
    /// </summary>
    public partial class BoxesControlContent : UserControl
    {
        public BoxesControlContent(Box obj)
        {
            InitializeComponent();
            this.DeleteUser.Visibility = obj.Rent == null ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            this.DataContext = obj;
        }

        /// <summary>
        /// Удалить аренду пользователя (для админа)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUserRent(object sender, System.Windows.RoutedEventArgs e)
        {
            var boxRent = (this.DataContext as Box).Rent;
            var db = App.Db;
            boxRent.EndDate = DateTime.Now;
            db.Update(boxRent);
            db.SaveChanges();
            Window.GetWindow(this).Close();
        }
    }
}
