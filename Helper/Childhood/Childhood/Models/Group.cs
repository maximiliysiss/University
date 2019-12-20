using System;
using System.Collections.Generic;
using System.Text;

namespace Childhood.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? TutorId { get; set; }
        public virtual User Tutor { get; set; }
        public virtual List<Child> Children { get; set; }
    }
}
