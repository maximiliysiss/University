using Chemical.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Chemical.Forms.Controls.Models
{
    /// <summary>
    /// Форма для списка моделей
    /// </summary>
    public abstract partial class BaseModelListControl : UserControl
    {
        public BaseModelListControl()
        {
            InitializeComponent();
            this.Loaded += (s, e) => Refresh();
        }

        /// <summary>
        /// Кнопка добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddClick(object sender, RoutedEventArgs e)
        {
            AddNew();
            Refresh();
        }

        /// <summary>
        /// Загрузить данные
        /// </summary>
        /// <returns></returns>
        protected abstract List<object> Load();
        /// <summary>
        /// Открыть
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void Open(object obj);
        /// <summary>
        /// Добавить новый
        /// </summary>
        protected abstract void AddNew();
        /// <summary>
        /// Данные
        /// </summary>
        protected List<object> data;
        /// <summary>
        /// Обновить
        /// </summary>
        public void Refresh() => DataGrid.ItemsSource = data = Load();
        /// <summary>
        /// Двойной клик по строке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Row_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Open((sender as DataGridRow).DataContext);
            Refresh();
        }
    }
}
