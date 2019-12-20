using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Childhood.Models;

namespace Childhood.Forms.Controls.Models.List
{
    /// <summary>
    /// Interaction logic for InformationList.xaml
    /// </summary>
    public partial class InformationList : UserControl
    {
        private List<Information> informations;

        public InformationList()
        {
            InitializeComponent();
            Reload();
        }

        private void Reload()
        {
            informations = App.Db.Information.ToList();
        }

        private void SaveMenu(object sender, RoutedEventArgs e)
        {

        }
    }
}
