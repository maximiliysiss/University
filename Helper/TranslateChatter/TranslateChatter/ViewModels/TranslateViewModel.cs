using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateChatter.ViewModels
{
    /// <summary>
    /// Модель для перевода
    /// </summary>
    public class TranslateViewModel
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
    }
}
