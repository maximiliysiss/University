using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chemical.UIElements
{
    /// <summary>
    /// DatePicker для валидации дат SQL (всегда > 1753 года)
    /// </summary>
    public class DateSqlPicker : DatePicker
    {
        public DateSqlPicker()
        {
            this.SelectedDate = DateTime.Today;
            this.PreviewTextInput += DateSqlPicker_PreviewTextInput;
        }

        /// <summary>
        /// Перед тем как установить дату
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateSqlPicker_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (DateTime.TryParse(e.Text, out var dateTime) && dateTime < SqlDateTime.MinValue)
                e.Handled = true;
        }
    }
}
