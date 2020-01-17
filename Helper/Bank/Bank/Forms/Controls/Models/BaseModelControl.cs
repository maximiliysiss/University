using Bank.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Bank.Forms.Controls.Models
{
    /// <summary>
    /// Окно для создания / редактирования
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseModelControl<T> : Window
    {
        /// <summary>
        /// Подключение к БД
        /// </summary>
        protected DatabaseContext databaseContext = App.Db;

        /// <summary>
        /// Кнопки
        /// </summary>
        public Button Action;
        public Button DeleteBtn;
        /// <summary>
        /// Таблица
        /// </summary>
        public Grid InnerContent;

        public BaseModelControl(T obj, UserControl content)
        {
            this.DataContext = obj;
            this.Width = content.Width + 50;
            this.Height = content.Height + 150;
            InitializeComponent();

            Action.Click += (s, e) => PrevAction(obj);

            if (IsEdit(obj))
            {
                Action.Content = "Изменить";
                Action.Click += (s, e) => databaseContext.Update(obj);
            }
            else
            {
                this.DeleteBtn.Visibility = Visibility.Hidden;
                Action.Content = "Добавить";
                Action.Click += (s, e) => databaseContext.Add(obj);
            }

            Action.Click += (s, e) =>
            {
                databaseContext.SaveChanges();
                Close();
            };

            this.InnerContent.Children.Add(content);
        }

        /// <summary>
        /// Пред действие
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void PrevAction(T obj)
        {
        }

        /// <summary>
        /// Создание формы
        /// </summary>
        private void InitializeComponent()
        {
            Grid grid = new Grid();
            grid.Margin = new Thickness(10);
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            InnerContent = new Grid();
            grid.Children.Add(InnerContent);

            Action = new Button();
            Action.SetValue(Grid.RowProperty, 1);

            DeleteBtn = new Button();
            DeleteBtn.Content = "Удалить";
            DeleteBtn.Click += DeleteClick;
            DeleteBtn.SetValue(Grid.RowProperty, 2);

            grid.Children.Add(DeleteBtn);
            grid.Children.Add(Action);

            this.Content = grid;
        }

        /// <summary>
        /// Форма для изменения?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract bool IsEdit(T obj);

        /// <summary>
        /// Кнопка удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            databaseContext.Remove(DataContext);
            databaseContext.SaveChanges();
            Close();
        }
    }
}
