using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HideColumn : System.Attribute
    {
        public HideColumn()
        {
        }
    }
}
