using Microsoft.EntityFrameworkCore;
using Production.Forms.Controls.Models.Model;
using Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Production.Forms.Controls.Models.List
{
    public class TeamUsersList : BaseModelListControl
    {
        private Team team;

        public TeamUsersList(Team team)
        {
            this.team = team;
        }

        protected override void AddNew() => Open(new UserInTeam { TeamId = team.ID, Team = team });

        protected override List<object> Load() => App.Db.UserInTeams.Include(x => x.Team).Where(x => x.TeamId == team.ID).Cast<object>().ToList();

        protected override void Open(object obj) => new TeamWorkerControl(obj as UserInTeam).ShowDialog();
    }
}
