using BookLibrary.Forms;
using BookLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BookLibrary
{
    /// <summary>
    /// Класс главного окна
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Путь до файла, где храниться библиотека
        /// </summary>
        public static string libraryPath = @"LibraryPath.xml";

        /// <summary>
        /// Список книг
        /// </summary>
        public List<Book> Books { get; set; } = new List<Book>();

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            // На всякий создаем папку под хранение изображений
            if (!Directory.Exists(AddEditForm.ImagePath))
                Directory.CreateDirectory(AddEditForm.ImagePath);
            LoadAllBook();
        }

        /// <summary>
        /// Загрузка книг
        /// </summary>
        public void LoadAllBook()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(libraryPath, FileMode.OpenOrCreate);
                Books = xmlSerializer.Deserialize(fileStream) as List<Book>;
            }
            catch (Exception) { }
            finally { fileStream.Close(); }

            // Установим в таблицу данные
            bookDataGridView.DataSource = Books;
            // Удалим и добавим событие (первое нужно, чтобы в итоге не было кучи событий 
            // (из-за этого может 50 раз открыться окно изменения))
            bookDataGridView.CellDoubleClick -= BookDataGridView_CellDoubleClick;
            bookDataGridView.CellDoubleClick += BookDataGridView_CellDoubleClick;

        }

        /// <summary>
        /// Открытие на dblclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // По индексу получаем книгу
            int index = e.RowIndex;
            var book = Books[index];
            var formAdd = new AddEditForm(book);
            // При закрытии формы надо проверить выбрал ли пользователь какое-нибудь действие
            formAdd.FormClosing += (sen, ev) =>
            {
                if (formAdd.IsReady || formAdd.IsDelete)
                {
                    // Если было удалено -> надо удалить изображение
                    if (formAdd.IsDelete)
                    {
                        File.Delete(book.ImagePath);
                        Books.Remove(book);
                    }
                    SaveAllBook();
                    LoadAllBook();
                }
            };
            // Показать окно
            formAdd.ShowDialog();
        }

        // Сохранить книги
        public void SaveAllBook()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            StreamWriter stream = new StreamWriter(new FileStream(libraryPath, FileMode.Truncate));
            xmlSerializer.Serialize(stream, Books);
            stream.Close();
        }

        // Обработка на добавление книги
        private void addNewBook_Click(object sender, EventArgs e)
        {
            // Схожая логика, как и при изменении
            var form = new AddEditForm();
            form.FormClosing += (s, ev) =>
            {
                if (form.IsReady)
                {
                    Books.Add(form.Book);
                    SaveAllBook();
                }
            };
            form.ShowDialog();
            // Загрузить новые книги
            LoadAllBook();
        }
    }
}
