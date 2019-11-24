using Production.Forms.Controls.Models.List;
using Production.Models;
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

namespace Production.Forms.Controls.Models.Model
{
    public class TeamControl : BaseModelControl<Team>
    {
        public TeamControl(Team obj) : base(obj, new TeamControlContent(obj))
        {
        }

        public TeamControl(Team obj, UserControl content) : base(obj, content)
        {
        }

        public override bool IsEdit(Team obj) => obj.ID != 0;
    }


    /// <summary>
    /// Interaction logic for TeamControlContent.xaml
    /// </summary>
    public partial class TeamControlContent : UserControl
    {
        public TeamControlContent(Team team)
        {
            InitializeComponent();
            if (team.ID != 0)
            {
                this.UsersIn.Visibility = Visibility.Visible;
                this.UsersIn.Children.Add(new TeamUsersList(team));
            }
            var db = App.Db;
            this.Users.ItemsSource = db.Users.Where(x => x.UserRole == UserRole.Brigadir).ToList();
            this.DataContext = team;
        }
    }
}
