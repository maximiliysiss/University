using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Services
{
    /// <summary>
    /// Аттрибут для перехода к следующей формы
    /// </summary>
    public class AttributeGoForm : System.Attribute
    {
        /// <summary>
        /// Название формы, к которой надо перейти
        /// </summary>
        public string NextForm { get; set; }
    }
}
