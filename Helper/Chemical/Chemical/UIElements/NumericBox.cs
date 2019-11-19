using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Chemical.UIElements
{
    public class NumericBox : TextBox
    {
        public NumericBox()
        {
            this.PreviewTextInput += NumericBox_PreviewTextInput;
        }

        private void NumericBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
