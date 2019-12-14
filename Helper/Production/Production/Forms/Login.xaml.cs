using Production.Forms.Controls;
using Production.Services;
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

namespace Production.Forms
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Сервис авторизации
        /// </summary>
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
                case Models.UserRole.Admin:
                    control = new AdminControl();
                    name = "Админ";
                    break;
                case Models.UserRole.Director:
                    control = new DirectorControl();
                    name = "Директор";
                    break;
                case Models.UserRole.Brigadir:
                    control = new BrigadirControl();
                    name = "Бригадир";
                    break;
                case Models.UserRole.Worker:
                    control = new WorkerControl();
                    name = "Работник";
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
