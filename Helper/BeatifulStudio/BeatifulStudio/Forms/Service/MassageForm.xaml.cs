﻿using BeatifulStudio.DataLayout;
using BeatifulStudio.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeatifulStudio.Forms.Service
{
    /// <summary>
    /// Interaction logic for MassageForm.xaml
    /// </summary>
    public partial class MassageForm : Page
    {
        public DatabaseContext DatabaseContext { get; set; } = new DatabaseContext();
        public MassageForm()
        {
            InitializeComponent();
            Data.ItemsSource = new ObservableCollection<DataLayout.Models.Service>(DatabaseContext.Services.Where(x => x.ServiceType == DataLayout.Models.ServiceType.Massage));
            Data.LoadingRow += Data_LoadingRow;
        }

        private void Data_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.MouseDoubleClick += Row_MouseDoubleClick;
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var context = (sender as DataGridRow).DataContext as DataLayout.Models.Service;
            RegisterOrder.Register(context, DatabaseContext.Users.FirstOrDefault(x => x.ID == App.User.ID), DatabaseContext);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginService.RemoveLogin();
            NavigationService.GoBack();
        }
    }
}
