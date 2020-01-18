using Bank.Services;
using System.Windows;

namespace Bank.Forms
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Сервис авторизации
        /// </summary>
        public ILoginService loginService = App.BankModules.Resolve<ILoginService>();
        public WindowsSelectorService windowsSelector = App.BankModules.Resolve<WindowsSelectorService>();

        public Login()
        {
            InitializeComponent();
            this.BankName.Content = App.SettingsInfo.BankName;
            this.Address.Content = App.SettingsInfo.Address;
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

            var wnd = windowsSelector.GetWindowByRole(loginRes.Role);
            Close();
            wnd.Show();
        }
    }
}
