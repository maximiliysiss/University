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
using System.Windows.Shapes;

namespace Production.Forms.Controls.Models.Model
{
    /// <summary>
    /// Interaction logic for AddDetail.xaml
    /// </summary>
    public partial class AddDetail : Window
    {
        public AddDetail()
        {
            InitializeComponent();
            this.Details.ItemsSource = App.Db.Details.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var detail = (Detail)Details.SelectedItem;
            int count = int.Parse(Count.Text);
            detail.Count += count;
            var db = App.Db;
            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.Add(new MakedDetail { DetailName = detail.Name, Count = count });
            db.SaveChanges();
            Close();
        }
    }
}
