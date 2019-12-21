using Childhood.Services;
using System.Windows;

namespace Childhood.Forms
{
    /// <summary>
    /// Форма входа
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
