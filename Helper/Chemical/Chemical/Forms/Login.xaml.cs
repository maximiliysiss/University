using Chemical.Forms.Controls;
using Chemical.Services;
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

namespace Chemical.Forms
{
    /// <summary>
    /// Форма логина
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Сервис авторизации
        /// </summary>
        public ILoginService loginService = App.ChemicalModules.Resolve<ILoginService>();

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
                case Models.UserRole.Storage:
                    control = new StorageControl();
                    name = "Ответсвенный за хранение";
                    break;
                case Models.UserRole.ProWorker:
                    control = new ProWorkerControl();
                    name = "Прораб";
                    break;
                case Models.UserRole.Techolog:
                    control = new TechnologControl();
                    name = "Технолог";
                    break;
            }

            var wnd = new MainWindow(control);
            wnd.Title = name;
            wnd.Show();
            Close();
        }
    }
}
