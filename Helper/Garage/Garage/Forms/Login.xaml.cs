using Garage.Forms.Controls;
using Garage.Services;
using System.Windows;
using System.Windows.Controls;

namespace Garage.Forms
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public ILoginService loginService = App.ProductionModule.Resolve<ILoginService>();

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

            UserControl control = null;
            string name = string.Empty;

            switch (loginRes.UserRole)
            {
                case Models.UserRole.User:
                    control = new UsersControl();
                    break;
                case Models.UserRole.HomeKeeper:
                    control = new HomeKeeperControl();
                    break;
            }

            var wnd = new MainWindow(control)
            {
                Title = name
            };
            Close();
            wnd.Show();
        }
    }
}
