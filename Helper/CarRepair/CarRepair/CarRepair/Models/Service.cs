using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepair.Models
{
    /// <summary>
    /// Услуга
    /// </summary>
    public class Service
    {
        public int ID { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }
    }
}
