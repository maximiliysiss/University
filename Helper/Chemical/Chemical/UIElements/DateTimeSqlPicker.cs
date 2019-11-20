using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chemical.UIElements
{
    public class DateSqlPicker : DatePicker
    {
        public DateSqlPicker()
        {
            this.SelectedDate = DateTime.Today;
            this.PreviewTextInput += DateSqlPicker_PreviewTextInput;
        }

        private void DateSqlPicker_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (DateTime.TryParse(e.Text, out var dateTime) && dateTime < SqlDateTime.MinValue)
                e.Handled = true;
        }
    }
}
