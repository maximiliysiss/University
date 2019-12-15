using Garage.Extensions;
using Garage.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Garage.Forms
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly ILoginService loginService = App.ProductionModule.Resolve<ILoginService>();
        public UserWindowService userWindow = App.ProductionModule.Resolve<UserWindowService>();

        public Register()
        {
            InitializeComponent();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            var loginString = login.Text.Trim();
            var passwordString = password.Password.Trim();
            var confirmString = confirm.Password.Trim();

            if (StringUtils.IsNullOrEmpty(loginString, passwordString, confirmString) || passwordString != confirmString)
            {
                MessageBox.Show("Заполните поля правильно", "Error");
                return;
            }

            var user = loginService.Register(loginString, passwordString);
            var wnd = userWindow.OpenUserWindow(user.UserRole);
            Close();
            wnd.Show();
        }
    }
}
