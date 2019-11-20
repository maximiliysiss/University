using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemical.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayGridName : System.Attribute
    {
        public DisplayGridName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
