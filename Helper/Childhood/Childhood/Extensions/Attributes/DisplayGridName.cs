using System;
using System.Collections.Generic;
using System.Text;

namespace Childhood.Extensions.Attributes
{
    /// <summary>
    /// Аттрибут для отображения имени колонки
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
