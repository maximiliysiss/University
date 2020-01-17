using Bank.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Bank.Extensions
{
    /// <summary>
    /// Расширение для DataGrid, чтобы скрывать колонки + устанавливать имена колонок
    /// </summary>
    public static class DataGridExtension
    {
        /// <summary>
        /// Проперти, по которому будет применяться логика к элементу
        /// </summary>
        public static readonly DependencyProperty HideAnnotatedColumnsProperty = DependencyProperty.RegisterAttached(
           "HideAnnotatedColumns",
           typeof(bool),
           typeof(DataGridExtension),
           new UIPropertyMetadata(false, OnHideAnnotatedColumns));

        public static bool GetHideAnnotatedColumns(DependencyObject d)
        {
            return (bool)d.GetValue(HideAnnotatedColumnsProperty);
        }

        public static void SetHideAnnotatedColumns(DependencyObject d, bool value)
        {
            d.SetValue(HideAnnotatedColumnsProperty, value);
        }

        private static void OnHideAnnotatedColumns(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool hideAnnotatedColumns = (bool)e.NewValue;

            DataGrid dataGrid = d as DataGrid;

            if (hideAnnotatedColumns)
            {
                dataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            }
            else
            {
                dataGrid.AutoGeneratingColumn -= dataGrid_AutoGeneratingColumn;
            }
        }

        private static void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = e.PropertyDescriptor as PropertyDescriptor;
            if (propertyDescriptor != null)
            {
                foreach (var item in propertyDescriptor.Attributes)
                {
                    if (item.GetType() == typeof(HideColumn))
                    {
                        e.Cancel = true;
                    }

                    if (item.GetType() == typeof(DisplayGridName))
                        e.Column.Header = (item as DisplayGridName).Name;
                }
            }
        }
    }
}
