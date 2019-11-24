using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Production.Forms.Controls.Models.List;
using Production.Models;

namespace Production.Forms.Controls.Models.Model
{
    public class WorkerTeamControl : TeamControl
    {
        public WorkerTeamControl(Team obj) : base(obj, new WorkerTeamControlContent(obj))
        {
            this.Action.Visibility = System.Windows.Visibility.Hidden;
            this.DeleteBtn.Visibility = System.Windows.Visibility.Hidden;
        }
    }

    public class WorkerTeamControlContent : TeamControlContent
    {
        public WorkerTeamControlContent(Team team) : base(team)
        {
            this.UsersIn.Children.Clear();
            this.UsersIn.Children.Add(new WorkerTeamUsersList(team));
        }
    }
}
