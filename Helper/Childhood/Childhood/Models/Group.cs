using Childhood.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Childhood.Models
{
    public class Group
    {
        [HideColumn]
        public int ID { get; set; }
        [DisplayGridName("Название")]
        public string Name { get; set; }
        [HideColumn]
        public int? TutorId { get; set; }
        [DisplayGridName("Воспитатель")]
        public virtual User Tutor { get; set; }
        [HideColumn]
        public virtual List<Child> Children { get; set; }

        public override string ToString() => Name;
    }
}
