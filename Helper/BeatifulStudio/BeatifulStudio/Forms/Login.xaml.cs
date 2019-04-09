using BeatifulStudio.DataLayout;
using BeatifulStudio.DataLayout.WIndows;
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

namespace BeatifulStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Page
    {
        public DatabaseContext DatabaseContext { get; set; } = new DatabaseContext();

        public Login()
        {
            InitializeComponent();
            Loaded += Login_Loaded;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            var loginModel = LoginService.TryGetLoginInfo();
            if (loginModel != null)
                LoginAttempt(loginModel, true);
        }

        private void LoginAttempt(LoginModel loginModel, bool inStart = false)
        {
            var user = DatabaseContext.Users.FirstOrDefault(x => x.Login == loginModel.Login &&
                                       x.Password == loginModel.Password);
            if (user == null)
            {
                if (!inStart)
                    MessageBox.Show("Incorrect login", "Error");
                return;
            }
            if (!inStart)
                LoginService.TryAddLogin(loginModel);
            App.User = user;
            string path = null;
            switch (user.Role)
            {
                case DataLayout.Models.Role.User:
                    path = "Forms/BeatifulStudio.xaml";
                    break;
                case DataLayout.Models.Role.Admin:
                    path = "Forms/AdminForm.xaml";
                    break;
                case DataLayout.Models.Role.Master:
                    path = "Forms/MasterPage.xaml";
                    break;
            }
            NavigationService.Navigate(new Uri(path, UriKind.Relative));
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            LoginAttempt(new LoginModel { Login = login.Text, Password = password.Password });
        }
    }
}
