using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolService.Models
{
    public class Teacher : User
    {
        public bool IsClassWork { get; set; }
        public virtual List<Class> Class { get; set; }
        public virtual List<Mark> Marks { get; set; }
    }
}
