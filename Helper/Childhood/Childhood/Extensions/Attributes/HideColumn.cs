using System;
using System.Collections.Generic;
using System.Text;

namespace Childhood.Extensions.Attributes
{
    /// <summary>
    /// Аттрибут для скрытия колонки
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HideColumn : System.Attribute
    {
        public HideColumn()
        {
        }
    }
}
