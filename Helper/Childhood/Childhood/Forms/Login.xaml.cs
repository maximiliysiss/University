using Childhood.Services;
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

namespace Childhood.Forms
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public ILoginService loginService = App.ChildhoodModule.Resolve<ILoginService>();
        public UserWindowService userWindow = App.ChildhoodModule.Resolve<UserWindowService>();

        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Кнопка Вход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var loginRes = loginService.LoginAttempt(login.Text.Trim(), password.Password.Trim());
            if (loginRes == null)
            {
                MessageBox.Show("Неверный логин/пароль", "Ошибка", MessageBoxButton.OK);
                return;
            }

            var wnd = userWindow.OpenUserWindow(loginRes.UserType);
            Close();
            wnd.Show();
        }
    }
}
