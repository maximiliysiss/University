using System.Collections.Generic;

namespace SchoolService.Models
{
    public class Class
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual List<Child> Children { get; set; }
    }
}