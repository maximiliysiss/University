using Microsoft.EntityFrameworkCore;
using Production.Forms.Controls.Models.Model;
using Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Forms.Controls.Models.List
{
    public class TeamList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Team());

        protected override List<object> Load() => App.Db.Teams.Include(x => x.Brigadir).Cast<object>().ToList();

        protected override void Open(object obj) => new TeamControl(obj as Team).ShowDialog();
    }

    public class UserTeamList : TeamList
    {
        readonly User brigadir;

        public UserTeamList(User brigadir)
        {
            this.brigadir = brigadir;
            this.Add.Visibility = System.Windows.Visibility.Hidden;
        }

        protected override List<object> Load() => App.Db.Teams.Include(x => x.Brigadir).Where(x => x.BrigadirId == brigadir.ID).Cast<object>().ToList();
    }

    public class WorkerTeamList : TeamList
    {
        private readonly User worker;

        public WorkerTeamList(User worker)
        {
            this.worker = worker;
            this.Add.Visibility = System.Windows.Visibility.Hidden;
        }

        protected override List<object> Load() => App.Db.UserInTeams.Include(x => x.Team).Where(x => x.WorkerId == worker.ID).Select(x => x.Team).Include(x=>x.Brigadir).Cast<object>().ToList();

        protected override void Open(object obj) => new WorkerTeamControl(obj as Team).ShowDialog();
    }
}
