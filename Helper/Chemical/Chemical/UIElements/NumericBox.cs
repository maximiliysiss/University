using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Chemical.UIElements
{
    /// <summary>
    /// Поле для чисел
    /// </summary>
    public class NumericBox : TextBox
    {
        public NumericBox()
        {
            this.PreviewTextInput += NumericBox_PreviewTextInput;
        }

        /// <summary>
        /// Ввод данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
