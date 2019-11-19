using Chemical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Chemical.Forms.Controls.Models
{
    public abstract class BaseModelControl<T> : Window
    {
        protected DatabaseContext databaseContext = App.ChemicalModules.Resolve<DatabaseContext>();

        public Button Action;
        public Button DeleteBtn;
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

        protected virtual void PrevAction(T obj)
        {
        }

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

        public abstract bool IsEdit(T obj);

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            databaseContext.Remove(DataContext);
            databaseContext.SaveChanges();
            Close();
        }
    }
}
