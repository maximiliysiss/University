using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Extensions.Attributes
{
    /// <summary>
    /// Аттрибут для правильного отображения названия колонки
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayGridName : System.Attribute
    {
        public DisplayGridName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
    }
}
