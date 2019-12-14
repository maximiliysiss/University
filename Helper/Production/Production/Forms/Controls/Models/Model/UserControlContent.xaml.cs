using Microsoft.EntityFrameworkCore;
using Production.Models;
using Production.Services;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace Production.Forms.Controls.Models.Model
{
    public class UsersControl : BaseModelControl<User>
    {
        public UsersControl(User obj) : base(obj, new UserControlContent(obj as User))
        {
        }

        public override bool IsEdit(User obj) => obj.ID != 0;

        protected override bool PrevAction(User obj)
        {
            base.PrevAction(obj);
            obj.Team = null;
            if (obj.UserRole != UserRole.Brigadir || obj.TeamId == 0)
                obj.TeamId = null;
            var userControl = this.InnerContent.Children[0] as UserControl;
            ((User)userControl.DataContext).PasswordHash = CryptService.CreateMD5(((User)userControl.DataContext).PasswordHash);
            return true;
        }
    }


    /// <summary>
    /// Interaction logic for UserControlContent.xaml
    /// </summary>
    public partial class UserControlContent : UserControl
    {
        public UserControlContent(User user)
        {
            InitializeComponent();
            var teams = App.Db.Teams.Where(x => x.Brigadir == null).ToList();
            if (user.Team != null)
                teams.Add(user.Team);
            if (teams.Count > 0 && user.TeamId != null)
                teams.Insert(0, new Team { ID = 0 });
            this.Teams.ItemsSource = teams;
            this.Team.Visibility = user.UserRole == UserRole.Brigadir ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            this.DataContext = user;
            this.Loaded += UserControlContent_Loaded;
        }

        private void UserControlContent_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RoleUser.SelectionChanged += RoleUser_SelectionChanged;
        }

        private void RoleUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var user = DataContext as User;
            this.Teams.ItemsSource = App.Db.Teams.Where(x => x.Brigadir == null).ToList();
            this.Team.Visibility = user.UserRole == UserRole.Brigadir ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
    }
}
