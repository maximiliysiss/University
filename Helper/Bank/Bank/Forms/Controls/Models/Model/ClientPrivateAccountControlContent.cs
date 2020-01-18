using Bank.Models;
using System.Windows.Controls;

namespace Bank.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма для лс клиента
    /// </summary>
    public class ClientPrivateAccountControl : PrivateAccountControl
    {
        public ClientPrivateAccountControl(PrivateAccount obj) : base(obj, new ClientPrivateAccountControlContent(obj))
        {
            // скроем кнопки
            var grid = this.Content as Grid;
            Action.Visibility = System.Windows.Visibility.Collapsed;
            DeleteBtn.Visibility = System.Windows.Visibility.Collapsed;
            grid.RowDefinitions.RemoveAt(grid.RowDefinitions.Count - 1);
            grid.RowDefinitions.RemoveAt(grid.RowDefinitions.Count - 1);
        }
    }

    /// <summary>
    /// Наполнение формы (верхний класс - чисто оболочка с парой кнопок)
    /// </summary>
    public class ClientPrivateAccountControlContent : PrivateAccountControlContent
    {
        public ClientPrivateAccountControlContent(PrivateAccount obj) : base(obj)
        {
            this.Currency.IsEnabled = false;
            this.Sum.IsEnabled = false;
            this.User.IsEnabled = false;
        }
    }
}
