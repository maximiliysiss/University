using Chemical.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Chemical.Forms.Controls.Models
{
    /// <summary>
    /// Interaction logic for BaseModelListControl.xaml
    /// </summary>
    public abstract partial class BaseModelListControl : UserControl
    {
        public BaseModelListControl()
        {
            InitializeComponent();
            this.Loaded += (s, e) => Refresh();
        }

        protected void AddClick(object sender, RoutedEventArgs e)
        {
            AddNew();
            Refresh();
        }
        protected abstract List<object> Load();
        protected abstract void Open(object obj);
        protected abstract void AddNew();

        protected List<object> data;
        public void Refresh() => DataGrid.ItemsSource = data = Load();

        private void Row_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Open((sender as DataGridRow).DataContext);
            Refresh();
        }
    }
}
