using Childhood.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Childhood.Forms.Controls.Models
{
    public abstract class BaseModelControl<T> : Window
    {
        protected DatabaseContext databaseContext = App.Db;

        /// <summary>
        /// Кнопки
        /// </summary>
        protected Button Action;
        protected Button DeleteBtn;
        /// <summary>
        /// Таблица
        /// </summary>
        protected Grid InnerContent;
        protected bool prevValidate;
        private bool successSave = true;
        /// <summary>
        /// Пользовательский контрол
        /// </summary>
        private readonly UserControl userContent;

        public BaseModelControl(T obj, UserControl content)
        {
            this.DataContext = obj;
            this.userContent = content;
            this.Width = content.Width + 50;
            this.Height = content.Height + 150;
            InitializeComponent();

            Action.Click += (s, e) => prevValidate = PrevAction(obj);

            if (IsEdit(obj))
            {
                Action.Content = "Изменить";
                Action.Click += (s, e) => { if (prevValidate) databaseContext.Update(obj); };
            }
            else
            {
                this.DeleteBtn.Visibility = Visibility.Hidden;
                Action.Content = "Добавить";
                Action.Click += (s, e) => { if (prevValidate) databaseContext.Add(obj); };
            }

            Action.Click += (s, e) =>
            {
                try
                {
                    databaseContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    successSave = false;
                }
            };

            Action.Click += (s, e) =>
            {
                if (successSave)
                    PostAction(obj);
                Close();
            };

            this.InnerContent.Children.Add(content);
        }

        /// <summary>
        /// Получить форму внутри
        /// </summary>
        protected UserControl UserControl => userContent;

        protected virtual void PostAction(T obj) { }

        /// <summary>
        /// Пред действие
        /// </summary>
        /// <param name="obj"></param>
        protected virtual bool PrevAction(T obj) => true;

        /// <summary>
        /// Создание формы
        /// </summary>
        private void InitializeComponent()
        {
            Grid grid = new Grid
            {
                Margin = new Thickness(10)
            };
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
