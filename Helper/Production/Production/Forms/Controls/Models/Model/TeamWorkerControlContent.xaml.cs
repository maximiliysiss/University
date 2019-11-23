using Production.Models;
using System.Linq;
using System.Windows.Controls;

namespace Production.Forms.Controls.Models.Model
{
    public class TeamWorkerControl : BaseModelControl<UserInTeam>
    {
        public TeamWorkerControl(UserInTeam obj) : base(obj, new TeamWorkerControlContent(obj))
        {
        }

        public override bool IsEdit(UserInTeam obj) => obj.ID != 0;

        protected override bool PrevAction(UserInTeam obj)
        {
            obj.Team = null;
            return true;
        }
    }

    /// <summary>
    /// Interaction logic for TeamWorkerControlContent.xaml
    /// </summary>
    public partial class TeamWorkerControlContent : UserControl
    {
        public TeamWorkerControlContent(UserInTeam worker)
        {
            InitializeComponent();
            this.Teams.ItemsSource = new[] { worker.Team };
            var usersInTeam = App.Db.UserInTeams.Where(x => x.TeamId == worker.TeamId && x.ID != worker.ID).Select(x => x.WorkerId).ToList();
            this.Users.ItemsSource = App.Db.Users.Where(x => x.UserRole == UserRole.Worker && !usersInTeam.Contains(x.ID)).ToList();
            this.DataContext = worker;
        }
    }
}
