using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Model
{
    /// <summary>
    /// Класс книги
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Book()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string ImagePath { get; set; }
        /// <summary>
        /// Установка нового пути для изображения
        /// Если путь новый, то старое изображение удалим
        /// </summary>
        public string ImagePathSystem
        {
            set
            {
                if (value != ImagePath)
                {
                    if (!string.IsNullOrEmpty(ImagePath))
                        File.Delete(ImagePath);
                    ImagePath = value;
                }
            }
        }
    }
}
