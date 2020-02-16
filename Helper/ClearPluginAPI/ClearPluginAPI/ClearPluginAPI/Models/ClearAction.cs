using System;

namespace ClearPluginAPI.Models
{
    /// <summary>
    /// Действие очистки
    /// </summary>
    public class ClearAction
    {
        public int ID { get; set; }
        /// <summary>
        /// Кто
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Когда
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }
    }
}
