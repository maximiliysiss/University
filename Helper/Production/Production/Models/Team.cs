using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Production.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int BrigadirId { get; set; }
        public User Brigadir { get; set; }
        public List<UserInTeam> Users { get; set; }
    }

    public class UserInTeam
    {
        public int ID { get; set; }
        public int WorkerId { get; set; }
        public User Worker { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
