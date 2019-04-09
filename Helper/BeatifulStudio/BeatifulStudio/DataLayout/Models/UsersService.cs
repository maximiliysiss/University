using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatifulStudio.DataLayout.Models
{
    public class UsersService
    {
        public int ID { get; set; }
        public User User { get; set; }
        public User Master { get; set; }
        public DateTime DateTime { get; set; }
        public Service Service { get; set; }
    }
}
