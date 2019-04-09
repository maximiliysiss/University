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

namespace BookLibrary.Forms
{
    public partial class AddEditForm : Form
    {
        /// <summary>
        /// Книга
        /// </summary>
        public Book Book { get; set; }
        /// <summary>
        /// Готова для сохранения или изменения
        /// </summary>
        public bool IsReady { get; set; } = false;
        /// <summary>
        /// Готов на удаление
        /// </summary>
        public bool IsDelete { get; set; } = false;
        /// <summary>
        /// Путь для сохранения изображений
        /// </summary>
        public static string ImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LibraryImages\\");

        /// <summary>
        /// Конструктор
        /// </summary>
        public AddEditForm()
        {
            InitializeComponent();
            Book = new Book { DateTime = dateTimePicker.MinDate };
            Init();
            addNew.Text = "Add";
        }

        /// <summary>
        /// Конструктор для изменения
        /// </summary>
        /// <param name="book"></param>
        public AddEditForm(Book book)
        {
            InitializeComponent();
            delete.Visible = true;
            this.Book = book;
            Init();
            addNew.Text = "Edit";
        }

        /// <summary>
        /// Инициализация полей по книге
        /// </summary>
        public void Init()
        {
            // Нужно писать только числа
            // Берем только числа из строки и превращаем их в число
            pageCountTextBox.TextChanged += (o, e) =>
            {
                var strChars = string.Join(string.Empty, pageCountTextBox.Text
                                    .Where(x => char.IsNumber(x)).Select(x => x.ToString()).ToArray());
                if (int.TryParse(strChars, out int num))
                    pageCountTextBox.Text = num.ToString();
                else
                    pageCountTextBox.Text = int.MaxValue.ToString();
            };
            // Запишем данные
            this.nameTextBox.Text = Book.Name;
            this.authorTextBox.Text = Book.Author;
            this.dateTimePicker.Value = Book.DateTime;
            this.pageCountTextBox.Text = Book.PageCount.ToString();
            // Если есть путь до изображения, то надо его вывести
            if (!string.IsNullOrEmpty(Book.ImagePath))
            {
                this.imagePathLabel.Text = Book.ImagePath.Length > 30 ? $"...{Book.ImagePath.Substring(Book.ImagePath.Length - 27)}" : Book.ImagePath;
                this.pictureBox.ImageLocation = Book.ImagePath;
            }
        }

        /// <summary>
        /// Кнопка добавления и изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNew_Click(object sender, EventArgs e)
        {
            // Получить данные
            Book.Author = authorTextBox.Text;
            Book.DateTime = dateTimePicker.Value;
            Book.Name = nameTextBox.Text;
            Book.PageCount = int.Parse(pageCountTextBox.Text);

            // Если что-то не заполнено, то выводим сообщение
            if (Book.Name.Trim().Length == 0 || Book.Author.Trim().Length == 0 || Book.DateTime == dateTimePicker.MinDate
                || Book.PageCount == 0)
            {
                MessageBox.Show("Incorrect book data", "Error");
                return;
            }

            // Получим информацию о изображении и о новом пути
            var fileInfo = new FileInfo(pictureBox.ImageLocation);
            var newPath = Path.Combine(ImagePath, fileInfo.Name);
            if (newPath != fileInfo.FullName)
            {
                try
                {
                    // Копируем на новый путь
                    fileInfo.CopyTo(newPath, true);
                    Book.ImagePathSystem = newPath;
                }
                catch (Exception ex)
                {
                    // Если ошибка, то пути нету
                    Book.ImagePath = string.Empty;
                }
            }
            this.IsReady = true;
            this.Close();
        }

        /// <summary>
        /// Изменение изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectImage(object sender, EventArgs e)
        {
            // Открываем диалог
            var res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                // Сохранить путь и отобразить
                var path = openFileDialog.FileName;
                pictureBox.ImageLocation = path;
                this.imagePathLabel.Text = path.Length > 30 ? $"...{path.Substring(path.Length - 27)}" : path;
            }
        }

        /// <summary>
        /// Кнопка удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_Click(object sender, EventArgs e)
        {
            IsDelete = true;
            this.Close();
        }
    }
}
