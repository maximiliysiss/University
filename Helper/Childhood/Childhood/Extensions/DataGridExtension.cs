using Childhood.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Childhood.Extensions
{
    public static class DataGridExtension
    {
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

        /// <summary>
        /// Скрыть лишнее или поставить название колонки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Если LazyLoader, то не надо выводить
            if (e.Column.Header.ToString() == "LazyLoader")
            {
                e.Cancel = true;
                return;
            }

            PropertyDescriptor propertyDescriptor = e.PropertyDescriptor as PropertyDescriptor;
            if (propertyDescriptor != null)
            {
                foreach (var item in propertyDescriptor.Attributes)
                {
                    if (item.GetType() == typeof(HideColumn))
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (item.GetType() == typeof(DisplayGridName))
                        e.Column.Header = (item as DisplayGridName).Name;
                }
            }
        }
    }
}
