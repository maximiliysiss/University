using System;
using System.Collections.Generic;
using System.Text;

namespace Childhood.Models
{
    /// <summary>
    /// Дополнительная информация
    /// </summary>
    public class Information
    {
        public int ID { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Информация
        /// </summary>
        public string Description { get; set; }
    }
}
