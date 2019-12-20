using System;
using System.Collections.Generic;
using System.Text;

namespace Childhood.Models
{
    public class Child
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Today;
        public string Address { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int? MomId { get; set; }
        public virtual User Mom { get; set; }
        public int? DaddyId { get; set; }
        public virtual User Daddy { get; set; }
    }
}
