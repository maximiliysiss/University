using BeatifulStudio.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeatifulStudio.Forms
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class BeatifulStudio : Page
    {
        public BeatifulStudio()
        {
            InitializeComponent();
        }

        private void BarberClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("Forms/Service/BarberShopForm.xaml", UriKind.Relative));
        }

        private void MassageClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("Forms/Service/MassageForm.xaml", UriKind.Relative));
        }

        private void ManicureClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("Forms/Service/ManicureForm.xaml", UriKind.Relative));
        }

        private void Logout(object sender, MouseButtonEventArgs e)
        {
            LoginService.RemoveLogin();
            NavigationService.GoBack();
        }
    }
}
